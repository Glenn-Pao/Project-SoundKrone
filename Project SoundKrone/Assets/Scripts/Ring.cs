using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour {
    public Foot foot;

    public Vector3 scaleStart;
    public Vector3 scaleEnd;
    public float scaletimer = 0.0f;
    public float scaleduration = 0.1f;
    public bool modeflag = true;    //refers to chosen

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.back, -0.5f);

        if (foot.bChosen && !modeflag)
        {
            modeflag = true;
            scaleStart = Vector3.zero;
            scaleEnd = Vector3.one;
            scaletimer = 0;
        }

        if (!foot.bChosen && modeflag)
        {
            modeflag = false;
            scaleStart = Vector3.one;
            scaleEnd = Vector3.zero;
            scaletimer = 0;
        }

        scaletimer += Time.deltaTime;
        transform.localScale = Vector3.Slerp(scaleStart, scaleEnd, scaletimer / scaleduration);
	}
}
