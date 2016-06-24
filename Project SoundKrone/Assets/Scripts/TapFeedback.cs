using UnityEngine;
using System.Collections;

//this class will handle the tap feedback. it will display what it needs to display based on the accuracy of the tap.
public class TapFeedback : MonoBehaviour {

    public Camera cam;
    public Bars[] arrImages;        //an array of bars
    int number = 4;                 //used to track the ID number to display the feedback.

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
        //RenderImage(Color.white);
	}

    //to be used in Foot class
    public void SetNumber(int num)
    {
        number = num;
    }

    //Based on the number given, render the image accordingly
    public void RenderImage(Color start)
    {
        //this array NEEDS 4 images
        //Perfect, Good, Bad and Miss
        Color end = cam.backgroundColor;
        Debug.Log("Number: " + number);
        switch(number)
        {
            case 0:     //Miss..
                arrImages[0].Flash(start, end);
                break;
            case 1:     //Good!
                arrImages[1].Flash(start, end);
                break;
            case 2:     //Perfect!
                arrImages[2].Flash(start, end);
                break;
            case 3:     //Bad..
                arrImages[3].Flash(start, end);
                break;
            default:
                //render nothing
                break;
        }
    }
}
