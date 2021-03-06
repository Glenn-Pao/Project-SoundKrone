﻿using UnityEngine;
using System.Collections;

//the feet as represented by blue and red buttons
public class Foot : MonoBehaviour
{

    float radius = 1;
    public Foot other;  //the other foot - set via inspector
    public bool bChosen;   //if true, the other object will
    public float angle;     //this updates as long as chosen, factoring in the base angle of 5
    float LastAngle;                //taken from the other's angle
    float LastBeat;                 //used to calculate movement

    float SnappedNextAngle; //transferred from other's SNS
    float SnappedLastAngle; //the angle you should press the button on
    public Conductor conductor;
    public Controller controller;
    public static bool bStationary = false;
    //public static bool bFlashEnabled = true;       //if true, show the flash.
    public BackgroundBars backgroundbars;
    public TapFeedback feedback;        //for visual feedback of perfect, good, bad, miss
    public Settings settings;           //the settings component. take out the flash feedback and check if it is true

    // Use this for initialization
    void Start()
    {
        if(conductor == null)
        {
            conductor = (Conductor)FindObjectOfType(typeof(Conductor));
        }
        if(settings == null)
        {
            settings = (Settings)FindObjectOfType(typeof(Settings));
        }
        angle = 0;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("stationary" + bStationary);
        if (bChosen && !controller.levelcleared && !Controller.isPaused)
        {
            //if the foot is not stationary.
            if (!bStationary)
                angle = SnappedLastAngle + ((((conductor.songposition - conductor.lasthit) / conductor.crotchet)
                                        * Mathf.PI * controller.speed + controller.angleoffset));
            SetOthersToAngle();
        }

        //as long as the level isn't cleared, give it a chance to fail
        if (Controller.isgameworld && Controller.started && conductor.beatnumber > 4 && !controller.levelcleared)
        {
            if (conductor.songposition - conductor.actuallasthit > conductor.crotchet * 2)
            {
                Debug.Log("Level failed");
                controller.FailLevel();
            }
        }
        if (radius > 0)
        {
            //trigger this when either faied or level is cleared
            if(Controller.failed || controller.levelcleared)
            {
                radius -= 0.05f;
            }
        }
    }
    //intended for the use of tap feedback to be placed in "SwitchChosen"
    void CollisionCheck()
    {
        //for tapping accuracy, written in order of: Early, Just Right, Late, Miss
        //Miss collision check occurs when all 3 of them are null
        Collider2D good = Physics2D.OverlapPoint(new Vector2(other.transform.position.x, other.transform.position.y), 1 << LayerMask.NameToLayer("Good"));
        Collider2D perfect = Physics2D.OverlapPoint(new Vector2(other.transform.position.x, other.transform.position.y), 1 << LayerMask.NameToLayer("Perfect"));
        Collider2D bad = Physics2D.OverlapPoint(new Vector2(other.transform.position.x, other.transform.position.y), 1 << LayerMask.NameToLayer("Bad"));

        //if it is not in the 'good' region, then it is either perfect or bad or miss
        if (good != null)
        {
            //flash the "GOOD!" sprite
            feedback.SetNumber(1);
        }
        if (perfect != null)
        {
            //flash the "PERFECT!" sprite
            feedback.SetNumber(2);
        }
        if (bad != null)
        {
            //flash the "BAD.." sprite
            feedback.SetNumber(3);
        }
        feedback.RenderImage(Color.white);
    }
    //called only on the chosen foot, when the button is pressed
    void BarFlash()
    {   
        backgroundbars.FlashBar(Color.grey);
    }
    public Foot SwitchChosen()
    {
        //check if there is a floor at the SNAPPED coordinate of other
        Vector3 snapped = SnappedCardinalDirection(SnapAngle(angle));

        Collider2D floor = Physics2D.OverlapPoint(new Vector2(snapped.x, snapped.y), 1 << LayerMask.NameToLayer("Floor"));


        //If there's no floor, it's a NO.
        //safecatch on total reversal of the game. in other words, going backwards when it's supposed to be going forward
        if (floor == null || (floor.GetComponent<Floor>().success && Controller.isgameworld)
            //(floor != null && other.transform.position.x < this.transform.position.x))
            )
        {
            //flash the "Miss.." sprite
            feedback.SetNumber(0);
            feedback.RenderImage(Color.white);

            if(settings.flashActivated)
            {
                backgroundbars.FlashBar(Color.red);
            }
            return this;
        }

        //level is cleared successfully
        if (floor.GetComponent<Floor>().isend)
        {
            controller.LevelCleared();
            return this;
        }

        //find out the exact location at which the player taps. This will give the feedback on how accurate their tap is.
        CollisionCheck();

        //if flash is enabled, trigger it
        if (settings.flashActivated)
        {
            backgroundbars.FlashBar(Color.grey);
        }

        //this is when player makes successful move, turn it to green
        floor.GetComponent<Floor>().success = true;
        

        SnappedNextAngle = SnapAngle(angle);
        other.SnappedLastAngle = SnappedNextAngle - Mathf.PI;
        conductor.actuallasthit = conductor.songposition;
        conductor.lasthit += (SnappedNextAngle - SnappedLastAngle) / Mathf.PI * conductor.crotchet / controller.speed;

        Debug.Log("lasthit " + conductor.lasthit);
        Debug.Log("songpos " + conductor.songposition);

        //swap the chosen one
        this.bChosen = false;
        other.bChosen = true;

        other.SnapToCardinalDirection(SnappedNextAngle);
        return other;
    }
    void SetOthersToAngle()
    {
        Vector3 temptranspos = this.transform.position;
        other.transform.position = new Vector3(temptranspos.x + Mathf.Sin(angle) * radius, temptranspos.y + Mathf.Cos(angle) * radius, temptranspos.z);
    }
    bool SnapToGrid()
    {
        Vector3 vec = transform.position;
        transform.position = new Vector3(Mathf.RoundToInt(vec.x), Mathf.RoundToInt(vec.y), 0);
        return true;
    }
    //Returns the coord of the cardinal direction at the snapped angle
    Vector3 SnappedCardinalDirection(float SnappedAngle)
    {
        //initialize offset to (0,0,0)
        Vector3 offsetfromchosenfoot = Vector3.zero;

        float integerscale = SnappedAngle / (Mathf.PI / 2.0f);
        int roundedint = Mathf.RoundToInt(integerscale);
        roundedint = roundedint % 4; //get its remainder so as to round it off.

        switch (roundedint)
        {
            case 0:
                offsetfromchosenfoot = new Vector3(0.0f, 1.0f, 0);
                break;

            case 1:
                offsetfromchosenfoot = new Vector3(1.0f, 0.0f, 0);
                break;

            case 2:
                offsetfromchosenfoot = new Vector3(0.0f, -1.0f, 0);
                break;

            case 3:
                offsetfromchosenfoot = new Vector3(-1.0f, 0.0f, 0);
                break;
        }

        return transform.position + offsetfromchosenfoot;

    }
    //Set the position of the planet to a cardinal direction from the other.
    void SnapToCardinalDirection(float snappedangle)
    {
        Vector3 offsetfromchosenfoot = Vector3.zero;

        float integerscale = snappedangle / (Mathf.PI / 2.0f);
        int roundedint = Mathf.RoundToInt(integerscale);
        roundedint = roundedint % 4;

        switch (roundedint)
        {
            case 0:
                offsetfromchosenfoot = new Vector3(0.0f, 1.0f, 0);
                break;

            case 1:
                offsetfromchosenfoot = new Vector3(1.0f, 0.0f, 0);
                break;

            case 2:
                offsetfromchosenfoot = new Vector3(0.0f, -1.0f, 0);
                break;

            case 3:
                offsetfromchosenfoot = new Vector3(-1.0f, 0.0f, 0);
                break;
        }

        transform.position = other.transform.position + offsetfromchosenfoot;

    }
    static public Vector3 RoundCoord(Vector3 coord)
    {
        return new Vector3(Mathf.RoundToInt(coord.x), Mathf.RoundToInt(coord.y), 0);
    }

    static public float SnapAngle(float angle)
    {
        float integerscale = angle / (Mathf.PI / 2.0f);
        int roundedint = Mathf.RoundToInt(integerscale);
        return roundedint * (Mathf.PI / 2);
    }
}
