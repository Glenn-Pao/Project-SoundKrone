using UnityEngine;
using System.Collections;

//this class handles the tools related to settings.
//this class shouldn't be destroyed
//to help facilitate maintaining the settings in place.
//upon initialization, it will have some default settings
public class Settings : MonoBehaviour {
    public float firstDigit = 0.0f;           //refers to the oneth number in the calibration component
    public float secondDigit = 8.0f;          //refers to the tenth number in the calibration component

    public bool pulseActivated = false;         //if true, pulse is activated
    public bool flashActivated = false;         //if true, flash is activated

    private static Settings instance = null;

    //read only, singleton approach
    public static Settings Instance
    {
        //return reference to private instance
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        if(instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        //make this active and only instance
        instance = this;

        DontDestroyOnLoad(gameObject);
    }
	// Use this for initialization
	void Start () 
    {
        pulseActivated = false;
        flashActivated = false;
	}
	
	// Update is called once per frame
    void Update()
    {

    }
}
