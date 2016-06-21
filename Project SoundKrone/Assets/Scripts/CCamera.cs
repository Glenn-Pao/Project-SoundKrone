using UnityEngine;
using System.Collections;

//Camera controls goes here
public class CCamera : MonoBehaviour
{
    GameObject following;  //who is it following?
    public Vector3 topos;
    public Vector3 frompos;
    public float timer;
    public float camsize = 5.0f;

    public float pulsemagnitude = 4.8f;     //effects
    float pulsetimer;
    float pulseduration = 0.2f;

    // Use this for initialization
    void Start()
    {
        topos = GetComponent<Camera>().transform.position;
        frompos = GetComponent<Camera>().transform.position;
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (Controller.isgameworld)
            GetComponent<Camera>().transform.position = Vector3.Slerp(frompos, topos, timer / 1.0f);
        
        pulsetimer += Time.deltaTime;
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(pulsemagnitude, camsize, pulsetimer / pulseduration);
	}
    //the kind of visual feedback this game needs
    public void Pulse()
    {
        pulsetimer = 0;
    }
    //when game over
    public void zoomout()
    {
        pulsetimer = 0;
        camsize = 100.0f;
        pulseduration = 10.0f;
    }
}
