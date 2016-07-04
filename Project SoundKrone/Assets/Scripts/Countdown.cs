using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//A one! A two! A one, two, three, four!
//You get it, right?
public class Countdown : MonoBehaviour
{
    public Camera cam;
    public Conductor conductor;     //need this to find out the beat count

    //Flash is used as it houses all the code needed to do transitioning between images.
    public Flash[] arrImages; //an array of images, you NEED 4 images for this to work!
    int count;                      //to track the count number

    void Awake()
    {
        if (conductor == null)
        {
            conductor = (Conductor)FindObjectOfType(typeof(Conductor));
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RenderImage(Color.white);
    }

    void RenderImage(Color start)
    {
        //define the count number
        count = 4 - conductor.beatnumber;
        Color end = cam.backgroundColor;

        //this array NEEDS 4 images in order to work!
        switch(count)
        {
            case 0:     //GO!
                arrImages[0].FlashColor(start, end);
                break;
            case 1:     //1..
                arrImages[arrImages.Length - 3].FlashColor(start, end);
                break;
            case 2:     //2..
                arrImages[arrImages.Length - 2].FlashColor(start, end);
                break;
            case 3:     //3..
                arrImages[arrImages.Length - 1].FlashColor(start, end);
                break;
        }
    }
}
