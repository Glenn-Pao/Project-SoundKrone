using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//A one! A two! A one, two, three, four!
//You get it, right?
public class Countdown : MonoBehaviour
{
    public Conductor conductor;
    public Text textStatus;
    //public Renderer image;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (conductor.beatnumber < 4)
        {
            //txtStatus.text = (4 - conductor.beatnumber).ToString();
        }
        else
        {
            //txtStatus.text = "GO!";
        }
    }

    void RenderImage()
    {
        //textStatus.text = (4 - conductor.beatnumber).ToString();
    }
}
