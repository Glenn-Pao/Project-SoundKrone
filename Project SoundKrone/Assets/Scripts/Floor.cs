using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

    public SpriteRenderer floorsprite;
    public Sprite star;
    public Color[] arrcolors;
    public bool flashycolor;
    public bool isend;
    public float speed = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Controller.started)
            floorsprite.color = Color.white;

        if (isend)
            floorsprite.sprite = star;

        OnBeat();
    }

    void OnBeat()
    {
        Debug.Log("On Beat");
        if (flashycolor)
            floorsprite.color = Color.green;
        else
            floorsprite.color = Color.white;

    }
}
