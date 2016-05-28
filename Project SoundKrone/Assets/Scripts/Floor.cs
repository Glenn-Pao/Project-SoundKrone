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
    }

    void OnBeat()
    {
        if (flashycolor)
            floorsprite.color = arrcolors[Mathf.FloorToInt(Random.Range(0, 5))];

    }
}
