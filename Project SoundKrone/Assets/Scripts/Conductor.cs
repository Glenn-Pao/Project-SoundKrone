using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//This class behaves like a conductor to synchronize both the gameplay and the music together during playback.
public class Conductor : MonoBehaviour {
    public AudioSource song;
    public AudioSource tick;
    public AudioClip tickclip;
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

    void DestroyObject()
    {
        Debug.Log("Destroy object");
        DestroyImmediate(this.gameObject);
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
                offset = 0.45f;
            //hasoffsetadjusted = true;
        }
        else
            offset = offsetstatic;
       
        //crotchet = SelectionBaseAttribute
        crotchet = 60.0f / bpm;
        AudioSource sounds = GetComponent<AudioSource>();
        song = sounds;

        //add in the controller script class here
        if (Controller.isgameworld)
            song.pitch = Controller.pitchchange;
        //Debug.Log("crotch" + crotchet);
        //Debug.Log(lasthit);
        nextbeattime = 0;
        nextbartime = 0;

        Debug.Log("Start Music");
        StartMusic();
	}
	void StartMusic()
    {
        song.Play();
    }
	// Update is called once per frame
	void Update () {
        crotchet = 60.0f / bpm;
        songposition = song.timeSamples / 44100.0f - offset;    //44100.0f refers to the song rate in Hz

        //once it goes across the next beat of music
        if(songposition > nextbeattime)
        {
            nextbeattime += crotchet;       //the incrementation will prevent lag from arising that happens every 1/60th of a second. It adds up..
            beatnumber++;
        }

        //once it goes across the next bar of music
        if(songposition > nextbartime)
        {
            nextbartime += crotchet * crotchetsperbar;
            barnumber++;
        }
       if (beatnumber >= 3 || !Controller.isgameworld)
        Controller.started = true;	
	}
}
