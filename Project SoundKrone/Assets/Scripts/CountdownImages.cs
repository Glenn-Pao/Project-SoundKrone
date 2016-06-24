using UnityEngine;
using System.Collections;

public class CountdownImages : MonoBehaviour {

    public Color colorStart;
    public Color colorEnd;
    public float colortimer = 0.0f;
    public float colorduration = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        colortimer += Time.deltaTime;

        GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, colortimer / colorduration);
	}

    public void FlashNumber(Color start, Color end)
    {
        colorStart = start;
        colorEnd = end;
        colortimer = 0.0f;
    }
}
