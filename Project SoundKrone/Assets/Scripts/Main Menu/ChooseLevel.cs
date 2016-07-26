using UnityEngine;
using System.Collections;

//This script handles the choosing of level
//It also affects the left and right button
public class ChooseLevel : MonoBehaviour {

    public MainMenu_PopOutButtons signal;           //to get the popOut component
    public MainMenu_ToggleButton[] arrLevels;       //the levels available
    int activeNum;                                  //the currently active display

	// Use this for initialization
	void Start () 
    {
        //set the rest to inactive for now
	    for(int i = 0; i < arrLevels.Length; i++)
        {
            arrLevels[i].gameObject.SetActive(!gameObject.activeInHierarchy);
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
        }
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
        }

        switch(activeNum)
        {
            case 0:         //Tutorial 2, disable all game objects to display default Tutorial 1
                break;
            case 1:         //Tutorial 3, switch to Tutorial 2
                arrLevels[0].gameObject.SetActive(gameObject.activeInHierarchy);
                break;
            case 10:        //display Tutorial 3
                arrLevels[1].gameObject.SetActive(gameObject.activeInHierarchy);
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
        }

        switch (activeNum)
        {
            case 0:         //Tutorial 2, switch to Tutorial 3
                arrLevels[1].gameObject.SetActive(gameObject.activeInHierarchy);
                break;
            case 1:         //Tutorial 3, switch to Tutorial 1. Disable all game objects
                break;
            case 10:         //display Tutorial 2
                arrLevels[0].gameObject.SetActive(gameObject.activeInHierarchy);
                break;
        }
    }
}
