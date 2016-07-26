using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//This class is the controller of the game. Any input by the player is captured here
public class Controller : MonoBehaviour
{
    public GameObject floor;

    public ScreenTransitions screentransitions;
   
    //implement this afterwards
    public Foot chosenfoot;             //left or right foot?
    public Conductor conductor;
    public CCamera theCamera;
    public float speed = 1;
    public string levelstring;
    public float angleoffset;
    public bool gameworld;
    public bool levelcleared;
    public static bool isgameworld;
    public static bool debug = true;
    public static bool started = false;
    public static float pitchchange = 0.8f;
    public static int currentlevel = 1;

    public static bool isPaused = false;       //is the game paused?
    public static bool failed = false;         //did the player fail the level?

    void Awake()
    {
        //a safecatch if I forget to add in theCamera object.
        if (theCamera == null)
        {
            theCamera = (CCamera)FindObjectOfType(typeof(Camera));
        }
        if (conductor == null)
        {
            conductor = (Conductor)FindObjectOfType(typeof(Conductor));
        }
    }
    // Use this for initialization
    public void Start()
    {
        //initialize all the variables here just in case I forget to intialize them again.
        started = false;
        levelcleared = false;
        failed = false;
        isPaused = false;
        isgameworld = gameworld;
        angleoffset = Mathf.PI / 2;        //base scale is 5

        //initialize the conductor's variables.
        //this is needed because of the singleton approach used. It will destroy the synergy between the song and gameplay if not handled properly
        Debug.Log("Stop song");
        conductor.song.Stop();      //stop the current song
        conductor.ResetStats();
        LoadSong();
        conductor.StartMusic();
    }
    //which song to load?
    void LoadSong()
    {
        if(Application.loadedLevelName.ToString() == "Tutorial_Level")
        {
            conductor.LoadSongLevel(0);     
        }
        else if (Application.loadedLevelName.ToString() == "Tutorial_Level_2")
        {
            conductor.LoadSongLevel(1);
        }
        else if(Application.loadedLevelName.ToString() == "Tutorial_Level_3")
        {
            conductor.LoadSongLevel(2);
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

    //press the pause button. This will trigger the pause.
    public void PauseGame()
    {
        //if not paused, toggle it to true
        if(!isPaused)
        {
            isPaused = true;
        }
        //if it is paused, toggle it back to false
        else
        {
            isPaused = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void FailLevel()
    {
        failed = true;
        conductor.song.Stop();  //stop the music
        //screentransitions.Quit();
    }

    public void LevelCleared()
    {
        levelcleared = true;
        levelCleared();
    }

    void levelCleared()
    {
        conductor.song.Stop();   //stop the music
        conductor.LoadSongLevel(5);
        conductor.StartMusic();
        screentransitions.SwitchToMainMenu();
    }
}
