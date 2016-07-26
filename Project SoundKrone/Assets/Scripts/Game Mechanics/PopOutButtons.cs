using UnityEngine;
using System.Collections;

//a super simple way of making the buttons pop out when needed.
public class PopOutButtons : MonoBehaviour
{
    bool popOut = false;
    public ToggleButton[] arrButtons;
    // Use this for initialization
    void Start()
    {
        //initialize the buttons affected to be inactive.
        for (int i = 0; i < arrButtons.Length; i++)
        {
            arrButtons[i].gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < arrButtons.Length; i++)
        {
            //show the buttons when the pause button is pressed OR the player failed the level
            if (popOut || Controller.failed)
            {
                arrButtons[i].gameObject.SetActive(gameObject.activeInHierarchy);
            }
            else if(!popOut || !Controller.failed)
            {
                arrButtons[i].gameObject.SetActive(!gameObject.activeInHierarchy);
            }
        }

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
