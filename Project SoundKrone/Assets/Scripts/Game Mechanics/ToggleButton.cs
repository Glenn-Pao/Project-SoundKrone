using UnityEngine;
using System.Collections;

//i don't know how else to do it.
//this is just a container class to check which buttons are affected in the pause state
//L.O.L
public class ToggleButton : MonoBehaviour {
    public Controller controller;
    public Conductor conductor;
    public PopOutButtons popOutButton;
    public ScreenTransitions transitions;
	// Use this for initialization
	void Start () 
    {
        if (conductor == null)
        {
            conductor = (Conductor)FindObjectOfType(typeof(Conductor));
        }
	}
	// Update is called once per frame
	void Update () 
    {
        
	}
    //to be used only on the restart button
    public void RestartButton()
    {
        //trigger this when the restart button is triggered
        popOutButton.setPopOut(false);
        controller.Start();
    }
    //to be used only on the main menu button
    public void MainMenuButton()
    {
        //change the song to Frozen Snow - Lullaby
        conductor.song.Stop();
        conductor.LoadSongLevel(3);
        conductor.StartMusic();
        controller.PauseGame();         //unpause the song tracking
        transitions.SwitchToMainMenu();
    }
}
