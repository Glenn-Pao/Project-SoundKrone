using UnityEngine;
using System.Collections;

//Red button control
public class ButtonMove : MonoBehaviour {

    public GameObject RedButton;        //the red button
    public Controller controller;            //the controller
    float transformYRed = 0;                       //bad code, it needs to be replaced with camera. For now, this should work.

	// Use this for initialization
	void Start () {
	
	}
	
    public void MoveRedButton()
    {
        if(controller.bLeftButtonTrigger)
        {
            //if cannot find it
            if (RedButton == null)
            {
                Debug.Log("Can't find Red Button.");
            }
            else
            {
                Debug.Log("Red Button Move");
                Debug.Log(transformYRed);
                //find the red button.
                RedButton = GameObject.FindGameObjectWithTag("Red Button");
                //basic transformation of red button upon click
                RedButton.transform.Translate(Vector3.up * Time.deltaTime);
                transformYRed += Time.deltaTime;
            }
        }
        if (controller.bLeftButtonTrigger && transformYRed > 1)
        {
            //reset the flag
            controller.bLeftButtonTrigger = false;
            transformYRed = 0;
        }
       
    }

	// Update is called once per frame
	void Update () {
        MoveRedButton();
	}
}
