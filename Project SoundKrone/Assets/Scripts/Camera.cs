using UnityEngine;
using System.Collections;

//Camera controls goes here
public class Camera : MonoBehaviour {
    GameObject following;  //who is it following?
    public Vector3 topos;
    public Vector3 frompos;
    public float timer;   
    public float camsize = 5.0f;

    //public float pulsemagnitude = 4.8f;     //effects
    //float pulsetimer;
    //float pulsedur = 0.2f;

	// Use this for initialization
	void Start () {
        topos = GetComponent<Camera>().transform.position;
        frompos = GetComponent<Camera>().transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (Controller.isgameworld)
            GetComponent<Camera>().transform.position = Vector3.Slerp(frompos, topos, timer / 1.0f);

        //pulsetimer += Time.deltaTime;
        //camera.orthographicSize = Mathf.Lerp(pulsemagnitude, camsize, pulsetimer / pulsedur);
	}
    //might not use this..
    public void Pulse()
    {

    }
    //when game over
    public void zoomout()
    {
        camsize = 100.0f;
    }
}
