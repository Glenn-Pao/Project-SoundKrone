using UnityEngine;
using System.Collections;

//NOT TO BE CONFUSED WITH BARS.CS
//This script handles flash specifically for visual feedback of "PERFECT", "GOOD", "BAD", "MISS"
public class TapFlash : MonoBehaviour {

    Vector3 LocalScale;                     //the local sclae of the feedback

    public Color colorStart;
    public Color colorEnd;
    public float colortimer = 0.0f;
    public float colorduration = 0.4f;

    // Use this for initialization
    void Start()
    {
        //Debug.Log("Start called");
        LocalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(LocalScale.x, transform.localScale.y - 0.1f, LocalScale.z);
        float newscale = (transform.localScale.y - LocalScale.y) * 0.2f;

        transform.localScale = new Vector3(LocalScale.x, newscale + LocalScale.y, LocalScale.z);

        colortimer += Time.deltaTime;

        //This general class should have an empty renderer component
        GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, colortimer / colorduration);
    }

    public void Flash(Color start, Color end)
    {
        colorStart = start;
        colorEnd = end;
        colortimer = 0.0f;
    }
}
