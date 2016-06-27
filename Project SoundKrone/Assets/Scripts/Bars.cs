using UnityEngine;
using System.Collections;

//This class is used as a visual feedback tool during play where depending on the player's input, it will flash either red or green for 0.4 seconds.
//It will then tell the user whether he loses a chance or succeeded in his button press
public class Bars : MonoBehaviour {

    public int frequency;                   //refers to the current sound's frequency level
    Vector3 LocalScale;                     //the local sclae of the bar
    public AudioSource ConductorAudio;      //the current song played right now
    float[] spectrum = new float[1024];     
    public float maxheight = 100;

    public Color colorStart;
    public Color colorEnd;
    public float colortimer = 0.0f;
    public float colorduration = 0.4f;

    // Use this for initialization
    void Start()
    {
        LocalScale = transform.localScale;
        frequency = Mathf.RoundToInt(transform.localPosition.x / 14 * 1000 + 20);
    }

    // Update is called once per frame
    void Update()
    {
        //Analyze the data provided. BlackmanHarris is used to reduce leakage of signals across frequency bands by providing a more accurate analysis of the window type
        //BlackmanHarris = 	W[n] = 0.35875 - (0.48829 * COS(1.0 * n/N)) + (0.14128 * COS(2.0 * n/N)) - (0.01168 * COS(3.0 * n/N))
        ConductorAudio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        transform.localScale = new Vector3(LocalScale.x, transform.localScale.y - 0.1f, LocalScale.z);
        float newscale = Mathf.Max(transform.localScale.y - LocalScale.y, spectrum[frequency] * maxheight);         //extend the bar based on how the sound wave looks like using the spectrum data

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
