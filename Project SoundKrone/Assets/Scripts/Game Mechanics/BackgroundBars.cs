using UnityEngine;
using System.Collections;

public class BackgroundBars : MonoBehaviour {
    public Bars[] arrBars;        //an array of bars
    public Camera cam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //this is used for visual feedback
    //To avoid confusion, FlashBar is used for flashing color FROM the this class.
    public void FlashBar(Color start)
    {
        for(int i = 0 ; i < arrBars.Length; i++)
        {
            Color end = cam.backgroundColor;
            arrBars[i].Flash(start, end);
        }
    }
}
