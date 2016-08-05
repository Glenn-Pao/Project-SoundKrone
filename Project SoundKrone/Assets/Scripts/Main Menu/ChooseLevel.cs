using UnityEngine;
using System.Collections;

//This script handles the choosing of level
//It also affects the left and right button
public class ChooseLevel : MonoBehaviour {

    int levelNum;                                   //this number will be passed in to controller and conductor
    public MainMenu_PopOutButtons signal;           //to get the popOut component
    public MainMenu_ToggleButton[] arrLevels;       //the levels available
    public MainMenu_ToggleButton[] arrImages;       //the corresponding images of the levels
    public ScreenTransitions transitions;           //screen transitions
    int activeNum;                                  //the currently active display

	// Use this for initialization
	void Start () 
    {
        activeNum = 0;
        levelNum = 0;
        //set the rest to inactive for now
	    for(int i = 0; i < arrLevels.Length; i++)
        {
            arrLevels[i].gameObject.SetActive(!gameObject.activeInHierarchy);
        }
        for (int i = 0; i < arrImages.Length; i++)
        {
            arrImages[i].gameObject.SetActive(!gameObject.activeInHierarchy);
        }
	}
	// Update is called once per frame
	void Update () 
    {
        //get the signal when popOut is not active
	    if(!signal.popOut)
        {
            for (int i = 0; i < arrLevels.Length; i++)
            {
                arrLevels[i].gameObject.SetActive(!gameObject.activeInHierarchy);
            }
            for (int i = 0; i < arrImages.Length; i++)
            {
                arrImages[i].gameObject.SetActive(!gameObject.activeInHierarchy);
            }
        }
        Debug.Log("Level Number: " + levelNum);
	}
    void FindActiveDisplay()
    {
        //find the active level displayed
        for (int i = 0; i < arrLevels.Length; i++)
        {
            if (arrLevels[i].gameObject.activeInHierarchy)
            {
                activeNum = i;
                break;
            }
            //indicate that there isn't any game objects active right now
            else if(i == arrLevels.Length - 1)
            {
                activeNum = 10;
            }
        }
    }
    public void LeftButtonPress()
    {
        FindActiveDisplay();

        Debug.Log("Active Num: " + activeNum);

        //there is a level apart from Tutorial 1 that is active
        if (activeNum != 10)
        {
            //make the currently active display inactive first.
            arrLevels[activeNum].gameObject.SetActive(!gameObject.activeInHierarchy);
            arrImages[activeNum].gameObject.SetActive(!gameObject.activeInHierarchy);
        }

        switch(activeNum)
        {
            case 0:         //Tutorial 2, disable all game objects to display default Tutorial 1
                levelNum = 0;
                break;
            case 1:         //Tutorial 3, switch to Tutorial 2
                levelNum = 1;
                arrLevels[0].gameObject.SetActive(gameObject.activeInHierarchy);
                arrImages[0].gameObject.SetActive(gameObject.activeInHierarchy);
                break;
            case 10:        //display Tutorial 3
                levelNum = 2;
                arrLevels[1].gameObject.SetActive(gameObject.activeInHierarchy);
                arrImages[1].gameObject.SetActive(gameObject.activeInHierarchy);
                break;
        }
    }
    public void RightButtonPress()
    {
        FindActiveDisplay();

        Debug.Log("Active Num: " + activeNum);

        //there is a level apart from Tutorial 1 that is active
        if(activeNum != 10)
        {
            //make the currently active display inactive first.
            arrLevels[activeNum].gameObject.SetActive(!gameObject.activeInHierarchy);
            arrImages[activeNum].gameObject.SetActive(!gameObject.activeInHierarchy);
        }

        switch (activeNum)
        {
            case 0:         //Tutorial 2, switch to Tutorial 3
                levelNum = 2;
                arrLevels[1].gameObject.SetActive(gameObject.activeInHierarchy);
                arrImages[1].gameObject.SetActive(gameObject.activeInHierarchy);
                break;
            case 1:         //Tutorial 3, switch to Tutorial 1. Disable all game objects
                levelNum = 0;
                break;
            case 10:         //display Tutorial 2
                levelNum = 1;
                arrLevels[0].gameObject.SetActive(gameObject.activeInHierarchy);
                arrImages[0].gameObject.SetActive(gameObject.activeInHierarchy);
                break;
        }
    }
    //the situation where play button is pressed
    public void PlayButtonPress()
    {
        //Based on the number passed in, load the level accordingly
        switch(levelNum)
        {
            case 0:
                transitions.SwitchToTutorialLevel();
                break;
            case 1:
                transitions.SwitchToTutorialLevel2();
                break;
            case 2:
                transitions.SwitchToTutorialLevel3();
                break;
        }
    }
}
