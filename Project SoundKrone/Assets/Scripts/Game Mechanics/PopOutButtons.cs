using UnityEngine;
using System.Collections;

//a super simple way of making the buttons pop out when needed.
public class PopOutButtons : MonoBehaviour
{
    bool popOut = false;
    public ToggleButton[] arrPauseButtons;          //when player triggers the pause button
    public ToggleButton[] arrLevelClearButtons;     //when player cleared the level
    public ToggleButton[] arrLevelFailedButtons;    //when player failed the level
    public Controller controller;                   //controller component

    // Use this for initialization
    void Start()
    {
        //initialize the buttons affected to be inactive.
        for (int i = 0; i < arrPauseButtons.Length; i++)
        {
            arrPauseButtons[i].gameObject.SetActive(!gameObject.activeInHierarchy);
        }
        //same goes for clear and failed buttons
        for(int j = 0; j < arrLevelClearButtons.Length; j++)
        {
            arrLevelClearButtons[j].gameObject.SetActive(!gameObject.activeInHierarchy);
        }
        for (int k = 0; k < arrLevelFailedButtons.Length; k++)
        {
            arrLevelFailedButtons[k].gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
    void UpdatePause()
    {
        if(!Controller.failed && !controller.levelcleared)
        {
            for (int i = 0; i < arrPauseButtons.Length; i++)
            {
                //show the buttons when the pause button is pressed OR the player failed the level
                if (popOut)
                {
                    arrPauseButtons[i].gameObject.SetActive(gameObject.activeInHierarchy);
                }
                else if (!popOut)
                {
                    arrPauseButtons[i].gameObject.SetActive(!gameObject.activeInHierarchy);
                }
            }
        }
    }
    //when player clears level
    void LevelClear()
    {
        if(controller.levelcleared)
        {
            //show cleared buttons
            for (int j = 0; j < arrLevelClearButtons.Length; j++)
            {
                arrLevelClearButtons[j].gameObject.SetActive(gameObject.activeInHierarchy);
            }
        }
    }
    //when player fails the level
    void LevelFail()
    {
        if(Controller.failed)
        {
            for (int k = 0; k < arrLevelFailedButtons.Length; k++)
            {
                arrLevelFailedButtons[k].gameObject.SetActive(gameObject.activeInHierarchy);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdatePause();
        LevelClear();       //this will trigger if level is successfully cleared
        LevelFail();        //this will trigger if level failed
    }
   
    public void ToggleButton()
    {
        //hide the button
        if (popOut)
        {
            popOut = false;

        }
        //show the button
        else
        {
            popOut = true;

        }
    }
    public void setPopOut(bool popOut)
    {
        this.popOut = popOut;
    }
    public bool GetPopOut()
    {
        return popOut;
    }
}
