using UnityEngine;
using System.Collections;

public class MainMenu_PopOutButtons : MonoBehaviour {

    public bool popOut = false;
    public MainMenu_ToggleButton[] arrPlayUI;              //when the play button is pressed, these game objects will be active
    public MainMenu_ToggleButton[] arrMainMenuButtons;     //these are the buttons active before the play button is pressed

	// Use this for initialization
	void Start () 
    {
	    //the play button components will all be inactive
        for(int i = 0; i < arrPlayUI.Length; i++)
        {
            arrPlayUI[i].gameObject.SetActive(!gameObject.activeInHierarchy);
        }
        //the main menu components will all be active
        for(int j = 0; j < arrMainMenuButtons.Length; j++)
        {
            arrMainMenuButtons[j].gameObject.SetActive(gameObject.activeInHierarchy);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        TogglePlayComponents();
        ToggleMainMenuComponents();
	}
    void TogglePlayComponents()
    {
        //the Play components
        for (int i = 0; i < arrPlayUI.Length; i++)
        {
            if (popOut)
            {
                arrPlayUI[i].gameObject.SetActive(gameObject.activeInHierarchy);
            }
            else
            {
                arrPlayUI[i].gameObject.SetActive(!gameObject.activeInHierarchy);
            }
        }
    }
    void ToggleMainMenuComponents()
    {
        //the Main Menu components, opposite effects of play UI
        for (int i = 0; i < arrMainMenuButtons.Length; i++)
        {
            if (popOut)
            {
                arrMainMenuButtons[i].gameObject.SetActive(!gameObject.activeInHierarchy);
            }
            else
            {
                arrMainMenuButtons[i].gameObject.SetActive(gameObject.activeInHierarchy);
            }
        }
    }
    public void TogglePlayButton()
    {
        //hide the play game UI
        if(popOut)
        {
            popOut = false;
        }
        //hide the main menu UI
        else
        {
            popOut = true;
        }
    }
}
