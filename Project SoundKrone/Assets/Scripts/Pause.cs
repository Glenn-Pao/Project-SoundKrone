using UnityEngine;
using System.Collections;

//a class that handles the pause o3o
//I think this is overkill though.
public class Pause : MonoBehaviour 
{
    public Renderer sprite;     //the sprite itself
    public Color start;         //starting color
    public Color end;           //ending color

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        ShowSprite();
	}

    public void ShowSprite()
    {
        if(Controller.isPaused)
        {
            sprite.material.color = Color.white;
        }
        else
        {
            sprite.material.color = Color.clear;
        }
    }
}
