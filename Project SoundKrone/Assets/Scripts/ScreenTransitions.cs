using UnityEngine;
using System.Collections;

//screen transitions class.
//there's a need to create a class for saved preferences. It will then be plugged in here
public class ScreenTransitions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
	
	}
    //simple loading of main menu
    public void SwitchToMainMenu()
    {
        Debug.Log("Load Main Menu");
        Application.LoadLevel("Main_Menu");
    }
    //load the how to play screen
    public void Help()
    {
        Debug.Log("Help Menu");
        Application.LoadLevel("Help");
    }
    //simple loading of tutorial level
    public void SwitchToTutorialLevel()
    {
        Debug.Log("Load Tutorial Level");
        Application.LoadLevel("Tutorial_Level");
    }
    //simple loading of tutorial level
    public void SwitchToTutorialLevel2()
    {
        Debug.Log("Load Tutorial Level 2");
        Application.LoadLevel("Tutorial_Level_2");
    }
    //simple loading of tutorial level
    public void SwitchToTutorialLevel3()
    {
        Debug.Log("Load Tutorial Level 3");
        Application.LoadLevel("Tutorial_Level_3");
    }
    //simple loading of level 1
    public void SwitchToLevel1()
    {
        Debug.Log("Load Level 1");
        Application.LoadLevel("Level_1");
    }
    //quit the application
    public void Quit()
    {
        Application.Quit();
    }
}
