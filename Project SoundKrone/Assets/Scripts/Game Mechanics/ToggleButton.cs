using UnityEngine;
using System.Collections;

//i don't know how else to do it.
//this is just a container class to check which buttons are affected in the pause state
//L.O.L
public class ToggleButton : MonoBehaviour {
    public Controller controller;
    public PopOutButtons popOutButton;
	// Use this for initialization
	void Start () {
	
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
}
