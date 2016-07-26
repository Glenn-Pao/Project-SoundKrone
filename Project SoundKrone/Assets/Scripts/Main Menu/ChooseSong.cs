using UnityEngine;
using System.Collections;

//choose your song for playback
//there will be a set of songs that can be played. Tutorial Level songs WILL NOT BE PLAYED
//this script is active only when the Main Menu UI is up
public class ChooseSong : MonoBehaviour {

    int activeSongNum;                              //the number in the array at which this display is active
    int activeComposerNum;                          //the number in the array at which this display is active
    public MainMenu_PopOutButtons signal;           //to get the popOut component
    public MainMenu_ToggleButton[] arrSongs;        //the song display
    public MainMenu_ToggleButton[] arrComposers;    //the composer's name for the songs
    public Conductor conductor;                     //controls the which song is played
    //for now, 1 song = 1 composer. This can and will change

	// Use this for initialization
	void Start () 
    {
        if (conductor == null)
        {
            conductor = (Conductor)FindObjectOfType(typeof(Conductor));
        }

	    //set everything to inactive first. Frozen Snow's Lullaby is not part of this
        for(int i = 0; i < arrSongs.Length; i++)
        {
            arrSongs[i].gameObject.SetActive(!gameObject.activeInHierarchy);
        }
        for (int j = 0; j < arrComposers.Length; j++)
        {
            arrComposers[j].gameObject.SetActive(!gameObject.activeInHierarchy);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	    //get the signal when popOut is active to disable all of them
        if(signal.popOut)
        {
            for (int i = 0; i < arrSongs.Length; i++)
            {
                arrSongs[i].gameObject.SetActive(!gameObject.activeInHierarchy);
            }
            for (int j = 0; j < arrComposers.Length; j++)
            {
                arrComposers[j].gameObject.SetActive(!gameObject.activeInHierarchy);
            }
        }
        else
        {
            //make sure to keep track of what is actively playing!
            if(conductor.tracknumber != 3)
            {
                switch(conductor.tracknumber)
                {
                    case 4:         //display Beethoven Moonlight Sonata
                        arrSongs[0].gameObject.SetActive(gameObject.activeInHierarchy);
                        arrComposers[0].gameObject.SetActive(gameObject.activeInHierarchy);
                        break;
                    case 5:         //display Mozart Turkish March
                        arrSongs[1].gameObject.SetActive(gameObject.activeInHierarchy);
                        arrComposers[1].gameObject.SetActive(gameObject.activeInHierarchy);
                        break;
                }
            }
        }
        ReplayTrackWhenNotPlaying();
	}
    void ReplayTrackWhenNotPlaying()
    {
        //when the song is no longer playing, just replay the song
        if(!conductor.song.isPlaying)
        {
            conductor.StartMusic();
        }
    }
    void FindActiveDisplay()
    {
        //find the active songs/composers displayed
        for(int i = 0; i < arrSongs.Length; i++)
        {
            if(arrSongs[i].gameObject.activeInHierarchy)
            {
                activeSongNum = i;
                break;
            }
            //indicate that there isn't any game objects active right now
            else if (i == arrSongs.Length - 1)
            {
                activeSongNum = 10;
            }
        }
        for(int j = 0; j < arrComposers.Length; j++)
        {
            if (arrComposers[j].gameObject.activeInHierarchy)
            {
                activeComposerNum = j;
                break;
            }
            //indicate that there isn't any game objects active right now
            else if (j == arrComposers.Length - 1)
            {
                activeComposerNum = 10;
            }
        }
    }
    void SwitchDisplayLeft()
    {
        conductor.song.Stop();
        //The song name switch
        switch(activeSongNum)
        {
            case 0:         //display Frozen Snow Lullaby
                conductor.LoadSongLevel(3);
                break;
            case 1:         //display Mozart's Turkish March
                arrSongs[0].gameObject.SetActive(gameObject.activeInHierarchy);
                conductor.LoadSongLevel(5);
                break;
            case 10:        //display Beethoven Moonlight Sonata
                arrSongs[1].gameObject.SetActive(gameObject.activeInHierarchy);
                conductor.LoadSongLevel(4);
                break;
        }
        //The composer name switch
        switch(activeComposerNum)
        {
            case 0:         //Frozen Snow
                break;
            case 1:         //Mozart
                arrComposers[0].gameObject.SetActive(gameObject.activeInHierarchy);
                break;
            case 10:        //Beethoven
                arrComposers[1].gameObject.SetActive(gameObject.activeInHierarchy);
                break;
        }
        conductor.StartMusic();
    }
    void SwitchDisplayRight()
    {
        conductor.song.Stop();
        //The song name switch
        switch (activeSongNum)
        {
            case 0:         //display Beethoven Moonlight Sonata
                arrSongs[1].gameObject.SetActive(gameObject.activeInHierarchy);
                conductor.LoadSongLevel(4);
                break;
            case 1:         //display Frozen Snow Lullaby
                conductor.LoadSongLevel(3);
                break;
            case 10:        //display Mozart Turkish March
                arrSongs[0].gameObject.SetActive(gameObject.activeInHierarchy);
                conductor.LoadSongLevel(5);
                break;
        }
        //The composer name switch
        switch (activeComposerNum)
        {
            case 0:         //Beethoven
                arrComposers[1].gameObject.SetActive(gameObject.activeInHierarchy);
                break;
            case 1:         //Frozen Snow 
                break;
            case 10:        //Mozart
                arrComposers[0].gameObject.SetActive(gameObject.activeInHierarchy);
                break;
        }
        conductor.StartMusic();
    }
    //when you press the left button..
    public void LeftButtonPress()
    {
        FindActiveDisplay();

        //there is another display apart from the default
        if(activeComposerNum != 10 && activeSongNum != 10)
        {
            //make the currently active display inactive first.
            arrSongs[activeSongNum].gameObject.SetActive(!gameObject.activeInHierarchy);
            arrComposers[activeComposerNum].gameObject.SetActive(!gameObject.activeInHierarchy);
        }
        SwitchDisplayLeft();
    }
    //when you press the right button..
    public void RightButtonPress()
    {
        FindActiveDisplay();

        //there is another display apart from the default
        if (activeComposerNum != 10 && activeSongNum != 10)
        {
            //make the currently active display inactive first.
            arrSongs[activeSongNum].gameObject.SetActive(!gameObject.activeInHierarchy);
            arrComposers[activeComposerNum].gameObject.SetActive(!gameObject.activeInHierarchy);
        }
        SwitchDisplayRight();
    }
}
