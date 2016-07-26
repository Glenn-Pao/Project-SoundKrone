using UnityEngine;
using System.Collections;

//This class is intended for the use of flash behaviour for visual feedback or game user interface features.
//This must be used in the object itself that can be used by Countdown classes.
//It doesn't seem to work with Bars class.
public class Flash : MonoBehaviour {

    public Color colorStart;
    public Color colorEnd;
    public float colortimer = 0.0f;
    public float colorduration = 0.4f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    //This is public function to allow other classes to utilize this update instead of writing the same code over multiple classes.
    public void Update()
    {
        colortimer += Time.deltaTime;

        //This general class should have an empty renderer component
        GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, colortimer / colorduration);
    }

    public void FlashColor(Color start, Color end)
    {
        colorStart = start;
        colorEnd = end;
        colortimer = 0.0f;
    }
}
