using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//This class behaves like a conductor to synchronize both the gameplay and the music together during playback.
public class Conductor : MonoBehaviour {
    public AudioSource song;
    public AudioSource tick;
    int crotchetsperbar = 8;                            //the smallest note is 1/8th in this situation. this can vary, but 1/8th should do for now
    public float bpm;                             //this can be changed

    public float crotchet;                              //this refers to the musical term that most recognize as "notes"
    public float songposition;                        //the current song position
    public float lasthit;                                //the last time this button is pressed (snapped to beat)
    public float actuallasthit;                       //the last time this button is pressed
    float nextbeattime = 0.0f;
    float nextbartime = 0.0f;
    public float offset = 0.2f;                      //positive means the song must be minussed!
    public static float offsetstatic = 0.2f;
    public static bool hasoffsetadjusted = false;
    public int beatnumber = 0;
    public int barnumber = 0;

    private static Conductor instance = null;

    //read only, singleton approach
    public static Conductor Instance
    {
        //return reference to private instance
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if(instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        //make this active and only instance
        instance = this;

        DontDestroyOnLoad(gameObject);
    }
	// Use this for initialization
	void Start () 
    {
        //adjust the playback based on the tiny lag that will be expected
        if (!hasoffsetadjusted)
        {
            if (Application.platform == RuntimePlatform.OSXWebPlayer)
                offset = 0.35f;
            if (Application.platform == RuntimePlatform.WindowsWebPlayer)
                offset = 0.45f;
            if (Application.platform == RuntimePlatform.Android)
                offset = 0.2f;
            hasoffsetadjusted = true;
        }
        else
        {
            offset = offsetstatic;
        }
       
        //crotchet = SelectionBaseAttribute
        crotchet = 60.0f / bpm;

        //add in the controller script class here
        if (Controller.isgameworld)
            song.pitch = Controller.pitchchange;
        //Debug.Log("crotch" + crotchet);
        //Debug.Log(lasthit);
        lasthit = 0;
        actuallasthit = 0;
        
        //StartMusic();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (!Controller.isPaused)
        {
            if(!song.isPlaying && !Controller.failed)
            {
                ResumeMusic();
            }
            crotchet = 60.0f / bpm;
            songposition = song.timeSamples / 44100.0f - offset;    //44100.0f refers to the song rate in Hz

            //once it goes across the next beat of music
            if (songposition > nextbeattime)
            {
                nextbeattime += crotchet;       //the incrementation will prevent lag from arising that happens every 1/60th of a second. It adds up..
                beatnumber++;
            }

            //once it goes across the next bar of music
            if (songposition > nextbartime)
            {
                nextbartime += crotchet * crotchetsperbar;
                barnumber++;
            }
            if (beatnumber >= 3 || !Controller.isgameworld)
                Controller.started = true;
        }
        if(Controller.isPaused)
        {
            PauseMusic();
        }
    }
    public void StartMusic()
    {
        song.Play();
    }
    public void LoadSongLevel()
    {
        AudioSource[] sounds = GetComponents<AudioSource>();

        if (Application.loadedLevelName.ToString() == "Tutorial_Level")
        {
            Debug.Log("Loaded Tutorial lv 1 sound");
            song = sounds[0];
        }
        else if (Application.loadedLevelName.ToString() == "Tutorial_Level_2")
        {
            Debug.Log("Loaded Tutorial lv 2 sound");
            song = sounds[1];
        }
        else if (Application.loadedLevelName.ToString() == "Tutorial_Level_3")
        {
            Debug.Log("Loaded Tutorial lv 3 sound");
            song = sounds[2];
        }
        if (Application.loadedLevelName.ToString() == "Level_1")
        {
            Debug.Log("Loaded Level 1 sound");
            bpm = 70;
            song = sounds[3];
        }
    }
    public void ResetStats()
    {
        Debug.Log("Reset Stats");
        lasthit = 0;
        actuallasthit = 0;
        beatnumber = 0;
        barnumber = 0;
        nextbeattime = 0;
        nextbartime = 0;
    }
    void PauseMusic()
    {
        song.Pause();
    }
    void ResumeMusic()
    {
        song.UnPause();
    }
}
