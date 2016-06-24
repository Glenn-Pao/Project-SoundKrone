using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//This class is the controller of the game. Any input by the player is captured here
public class Controller : MonoBehaviour
{
    public GameObject floor;
   
    //implement this afterwards
    public Foot chosenfoot;             //left or right foot?
    public Conductor conductor;
    public CCamera theCamera;
    public float speed = 1;
    public string levelstring;
    public float angleoffset;
    public bool gameworld;
    public bool failed;
    public static bool isgameworld;
    public static bool debug = true;
    public static bool started = false;
    public static float pitchchange = 0.8f;
    public static int currentlevel = 1;

    //public string[] captions = { };

    // Use this for initialization
    void Start()
    {
        started = false;
        isgameworld = gameworld;
        angleoffset = Mathf.PI / 2;        //base scale is 5

        //a safecatch if I forget to add in theCamera object.
        if(theCamera == null)
        {
            theCamera = (CCamera)FindObjectOfType(typeof(Camera));
        }
    }

    //This function serves to use common code used between "ButtonPressBlack" and "ButtonPressWhite
    void UponPress()
    {
        //trigger the pulse as visual feedback
        theCamera.timer = 0;
        theCamera.Pulse();

        theCamera.frompos = theCamera.transform.position;
        theCamera.topos = new Vector3(chosenfoot.transform.position.x, chosenfoot.transform.position.y, theCamera.transform.position.z);
    }

    //press that button, black color.
    //in this case, this button only works when the chosen foot is black.
    //it must not work when the current foot is white.
    public void ButtonPressBlack()
    {
        if (chosenfoot.name == "White Button" && started && !failed)
        {
            chosenfoot = chosenfoot.SwitchChosen();

            UponPress();
        }
    }
    //press that button, white color.
    //in this case, this button only works when the chosen foot is white.
    //it must not work when the current foot is black.
    public void ButtonPressWhite()
    {
        if (chosenfoot.name == "Black Button" && started && !failed)
        {
            chosenfoot = chosenfoot.SwitchChosen();

            UponPress();
        }
    }

    // Update is called once per frame
    void Update()
    {
       ////if(started)
       ////{
       ////    Debug.Log("Started!");
       ////}
       ////else
       ////{
       ////    Debug.Log("Haven't started!");
       ////}
       // if (Input.anyKeyDown && failed)
       // {
       //     //load the game again
       // }
       // if (Input.GetKey(KeyCode.Escape))
       // {
       //     //go to splash screen
       // }
    }

    public void FailLevel()
    {
        failed = true;
        conductor.GetComponent<AudioSource>().Pause();  //pause the music
    }
}
