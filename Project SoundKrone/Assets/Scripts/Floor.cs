using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

    public SpriteRenderer floorsprite;
    public Sprite star;
    public bool success;
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
        if (success)
            floorsprite.color = Color.green;

    }
}
