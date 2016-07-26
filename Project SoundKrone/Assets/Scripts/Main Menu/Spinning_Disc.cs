using UnityEngine;
using System.Collections;

//This is the graphics component of the spinning disc you will see in the main menu
public class Spinning_Disc : MonoBehaviour {

    float radius = 1;
    public Spinning_Disc other;     //the other 'disc'
    public bool bChosen;            //if true, the other will spin around it
    public float angle;             //update when chosen

    float SnappedLastAngle = 0;         //the angle it is currently at
    public Conductor conductor;     //the music player


	// Use this for initialization
	void Start () 
    {
        if (conductor == null)
        {
            conductor = (Conductor)FindObjectOfType(typeof(Conductor));
        }
        angle = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (bChosen)
        {
            angle = SnappedLastAngle + ((((conductor.songposition - conductor.lasthit) / conductor.crotchet) * Mathf.PI / 2));
            SetOthersToAngle();
        }
	}
    void SetOthersToAngle()
    {
        Vector3 temptranspos = this.transform.position;
        other.transform.position = new Vector3(temptranspos.x + Mathf.Sin(angle) * radius, temptranspos.y + Mathf.Cos(angle) * radius, temptranspos.z);
    }
}
