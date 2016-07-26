using UnityEngine;
using System.Collections;

//This is a graphical feature to indicate that the active button is spinning. It will give the player a rough idea at the start of the game.
public class Button_Ring : MonoBehaviour {
    public Foot foot;   //the foot

    public Vector3 scaleStart;
    public Vector3 scaleEnd;
    public float timer = 0.0f;
    public float duration = 0.1f;
    public bool bActive = true; //true = chosen foot
    
	// Use this for initialization
	void Start () 
    {
	
	}
	// Update is called once per frame
	void Update () 
    {
        //Ring rotates backwards
        transform.Rotate(Vector3.back, 0.5f);

        if(foot.bChosen && !bActive)
        {
            bActive = true;
            scaleStart = Vector3.zero;
            scaleEnd = Vector3.one;
            timer = 0;
        }
        if(!foot.bChosen && bActive)
        {
            bActive = false;
            scaleStart = Vector3.one;
            scaleEnd = Vector3.zero;
            timer = 0;
        }

        timer += Time.deltaTime;
        transform.localScale = Vector3.Slerp(scaleStart, scaleEnd, timer / duration);
	}
}
