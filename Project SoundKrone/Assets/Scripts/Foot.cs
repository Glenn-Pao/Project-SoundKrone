using UnityEngine;
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
    //bool flashed = false;
    public int numOfBeats;
    int currentCount = 0;                   //the current tile it is in
    public GameObject[] floor;           //refers to floor tiles

    // Use this for initialization
    void Start()
    {
        Debug.Log("Other chosen: " + other.name + " " + other.bChosen);
        for (int i = 0; i < numOfBeats; i++)
        {
            floor[i] = GameObject.Find("Red Beat (" + i + ")");
        }

    }
    //use the AABB collision to check for whether the other foot collides.
    bool CheckCollision(float posX, float posY, float nextPosX, float nextPosY, int spriteSize)
    {
        //it must go through this set of statements to try and prove no collision.
        if (posX < nextPosX)
            return false;
        else if (posX > nextPosX + spriteSize)
            return false;
        else if (posY < nextPosY)
            return false;
        else if (posY > nextPosY + spriteSize)
            return false;

        //if all the statements fail, then there is collision
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("stationary" + bStationary);
        if (bChosen)
        {
            //if the foot is not stationary.
            if (!bStationary)
                angle = SnappedLastAngle + ((((conductor.songposition - conductor.lasthit) / conductor.crotchet)
                                        * Mathf.PI * controller.speed + controller.angleoffset));
            SetOthersToAngle();
        }


        if (Controller.isgameworld && Controller.started && conductor.beatnumber > 4)
        {
            if (conductor.songposition - conductor.actuallasthit > conductor.crotchet * 2)
            {
                Debug.Log("Level failed");
                controller.FailLevel();
            }
        }
        if (controller.failed && radius > 0)
            radius -= 0.05f;
    }

    //called only on the chosen foot, when the button is pressed
    public Foot SwitchChosen()
    {
        //check if there is a floor at the SNAPPED coordinate of other
        Vector3 snapped = SnappedCardinalDirection(SnapAngle(angle));

        //this is when player makes successful move
        SnappedNextAngle = SnapAngle(angle);
        other.SnappedLastAngle = SnappedNextAngle - Mathf.PI;
        conductor.actuallasthit = conductor.songposition;
        conductor.lasthit += (SnappedNextAngle - SnappedLastAngle) / Mathf.PI * conductor.crotchet / controller.speed;

        Debug.Log("lasthit " + conductor.lasthit);
        Debug.Log("songpos " + conductor.songposition);
        Debug.Log("SWAPPED!");

        Debug.Log("other " + other.name);

        //swap the chosen one
        this.bChosen = false;
        other.bChosen = true;

        other.SnapToCardinalDirection(SnappedNextAngle);
        currentCount++;
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
