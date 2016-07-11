using UnityEngine;
using System.Collections;

//a class that handles the pause o3o
//I think this is overkill though.
public class Pause : MonoBehaviour 
{
    public Flash[] arrImages;
    //Color start = Color.clear;

	// Use this for initialization
	void Start () 
    {
        //ShowSprite(Color.clear);
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public void ShowSprite()
    {
        for (int i = 0; i < arrImages.Length; i++)
        {
            //initialize the 2 colors needed
            Color start, end;

            if(Controller.isPaused)
            {
                //define the colors needed
                start = Color.clear;
                end = Color.white;
                arrImages[i].FlashColor(start, end);
            }
            else
            {
                //define the colors needed
                start = Color.white;
                end = Color.clear;
                arrImages[i].FlashColor(start, end);
            }
            
        }
    }
}
