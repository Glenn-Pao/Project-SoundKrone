using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//This class is the controller of the game. Any input by the player is captured here
public class Controller : MonoBehaviour
{
    public bool bLeftButtonTrigger = false;        //trigger left button
    public bool bRightButtonTrigger = false;        //trigger right button
    public GameObject floor;
   
    //implement this afterwards
    public Foot chosenfoot;             //left or right foot?
    public Conductor conductor;
    public Camera theCamera;
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

        if(theCamera == null)
        {
            theCamera = (Camera)FindObjectOfType(typeof(Camera));

            if (theCamera)
                Debug.Log("Camera found");
            else
                Debug.Log("No camera found");
        }

        //MakeLevel(levelstring);
    }

    //press that button
    public void ButtonPress()
    {
        if (started && !failed)
        {
            chosenfoot = chosenfoot.SwitchChosen();

            theCamera.frompos = theCamera.transform.position;
            theCamera.topos = new Vector3(chosenfoot.transform.position.x, chosenfoot.transform.position.y, theCamera.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.anyKeyDown && failed)
        {
            //load the game again
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            //go to splash screen
        }
    }

    public void FailLevel()
    {
        failed = true;
        conductor.GetComponent<AudioSource>().Pause();  //pause the music
    }
    //void MakeLevel(string levelstring)
    //{
    //    //LEVEL MAKER LEVEL MAKER MAKE ME A LEVEL
    //    int xcoo = 0;
    //    int ycoo = 0;
    //    //Instantiate(floor, Vector3.zero, new Quaternion());
    //    bool newfloor = true;


    //    for (int i = 0; i < levelstring.Length; i++)
    //    {
    //        switch (levelstring[i])
    //        {
    //            case 'R':
    //                xcoo++;
    //                break;

    //            case 'L':
    //                xcoo--;
    //                break;

    //            case 'U':
    //                ycoo++;
    //                break;

    //            case 'D':
    //                ycoo--;
    //                break;
    //        }

    //       // Object flor = Instantiate(floor, new Vector3(xcoo, ycoo, 0), new Quaternion());

    //        /*if (levelstring[i+1]=='S')
    //        {
    //            (flor as GameObject).GetComponent<scrFloor>().speed = 0.25f;
    //            i++;
    //        }*/

    //        //if (i == levelstring.Length - 1)
    //            //(flor as GameObject).GetComponent<Floor>().isend = true;

    //    }


    //    return;

    //}
}
