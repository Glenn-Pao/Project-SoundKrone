using UnityEngine;
using System.Collections;

//the render manager when inside the settings page
public class Settings_RenderManager : MonoBehaviour {
    public Settings_PopOutSprites[] firstDigits;    //the oneth numbers
    public Settings_PopOutSprites[] secondDigits;   //the tenth numbers
    public Settings_PopOutSprites[] pulseTickboxes; //the pulse tickbox
    public Settings_PopOutSprites[] flashTickboxes; //the flash tickbox
    public Settings_PopOutSprites[] WarningSign;    //the warning sign
    public Settings settings;                       //the settings class
    public Conductor conductor;                     //the conductor class

    int firstNum;                                   //the local copy of oneth digit
    int secondNum;                                  //the local copy of tenth digit
    bool pulse;                                     //the local copy of pulseActivated
    bool flash;                                     //the local copy of flashActivated

	// Use this for initialization
	void Start () 
    {
        if (settings == null)
        {
            settings = (Settings)FindObjectOfType(typeof(Settings));
        }
        if(conductor == null)
        {
            conductor = (Conductor)FindObjectOfType(typeof(Conductor));
        }
        //copy over the settings's flags
        pulse = settings.pulseActivated;
        flash = settings.flashActivated;

        if(pulse || flash)
        {
            //display warning sign if either pulse and flash is active
            WarningSign[0].gameObject.SetActive(gameObject.activeInHierarchy);
        }
        else if(!pulse || !flash)
        {
            //no need to display if neither of them are active
            WarningSign[0].gameObject.SetActive(!gameObject.activeInHierarchy);
        }

        if(pulse)
        {
            //don't render the empty tickbox
            pulseTickboxes[0].gameObject.SetActive(!gameObject.activeInHierarchy);
            //render the pulse tickbox
            pulseTickboxes[1].gameObject.SetActive(gameObject.activeInHierarchy);
        }
        else
        {
            //render the empty tickbox
            pulseTickboxes[0].gameObject.SetActive(gameObject.activeInHierarchy);
            //don't render the pulse tickbox
            pulseTickboxes[1].gameObject.SetActive(!gameObject.activeInHierarchy);
        }

        if (flash)
        {
            //don't render the empty tickbox
            flashTickboxes[0].gameObject.SetActive(!gameObject.activeInHierarchy);
            //render the flash tickbox
            flashTickboxes[1].gameObject.SetActive(gameObject.activeInHierarchy);
        }
        else
        {
            //render the empty tickbox
            flashTickboxes[0].gameObject.SetActive(gameObject.activeInHierarchy);
            //don't render the flash tickbox
            flashTickboxes[1].gameObject.SetActive(!gameObject.activeInHierarchy);
        }

	    //all button components will all be inactive
        for (int i = 0; i < firstDigits.Length; i++)
        {
            //keep the current float number active
            if(i == (int)settings.firstDigit)
            {
                firstDigits[i].gameObject.SetActive(gameObject.activeInHierarchy);
            }
            else
            {
                firstDigits[i].gameObject.SetActive(!gameObject.activeInHierarchy);
            }
        }
        for (int j = 0; j < secondDigits.Length; j++)
        {
            //keep the current float number active
            if (j == (int)settings.secondDigit)
            {
                secondDigits[j].gameObject.SetActive(gameObject.activeInHierarchy);
            }
            else
            {
                secondDigits[j].gameObject.SetActive(!gameObject.activeInHierarchy);
            }
        }
        
	}
	// Update is called once per frame
	void Update () 
    {
	
	}
    //update the gameplay settings based on the user feedback here
    public void UpdateGameplay()
    {
        //update the offset
        conductor.offset = (settings.firstDigit * 0.1f) + (settings.secondDigit * 0.01f);
        pulse = settings.pulseActivated;        //update the pulse
        flash = settings.flashActivated;        //update the flash
    }
    //find the active disply of the first number
    void FindActiveFirstNumber()
    {
        //find the active level displayed
        for (int i = 0; i < firstDigits.Length; i++)
        {
            if (firstDigits[i].gameObject.activeInHierarchy)
            {
                firstNum = i;
                break;
            }
        }
    }
    //derender the numbers
    void DerenderNumbers()
    {
        //disable the current numbers
        firstDigits[firstNum].gameObject.SetActive(!gameObject.activeInHierarchy);
        secondDigits[secondNum].gameObject.SetActive(!gameObject.activeInHierarchy);
    }
    //render the numbers
    void RenderNumbers()
    {
        //enable the new numbers
        firstDigits[firstNum].gameObject.SetActive(gameObject.activeInHierarchy);
        secondDigits[secondNum].gameObject.SetActive(gameObject.activeInHierarchy);
    }
    //find the active disply of the second number
    void FindActiveSecondNumber()
    {
        //find the active level displayed
        for (int i = 0; i < secondDigits.Length; i++)
        {
            if (secondDigits[i].gameObject.activeInHierarchy)
            {
                secondNum = i;
                break;
            }
        }
    }
    //when the up button is pressed
    public void UpButtonPress()
    {
        FindActiveFirstNumber();        //find the oneth digit
        FindActiveSecondNumber();       //find the tenth digit
        DerenderNumbers();              //derender the numbers first

        //check for the right extreme end of the tenth digit
        if(secondNum == 9)
        {
            //the final number of first number shouldn't be active for this to work
            if(firstNum != 9)
            {
                secondNum = 0;     //reset the second number.
                firstNum += 1;     //increment the first number
            }
            //this is the maximum setting possible
            else
            {
                secondNum = 9;
                firstNum = 9;
            }
        }
        //increment the second number if doesnt exceed 9
        else
        {
            secondNum += 1;
        }
        //keep the settings script updated
        settings.firstDigit = (float)firstNum;
        settings.secondDigit = (float)secondNum;

        //render the new numbers
        RenderNumbers();
    }
    //when the down button is pressed
    public void DownButtonPress()
    {
        FindActiveFirstNumber();        //find the oneth digit
        FindActiveSecondNumber();       //find the tenth digit
        DerenderNumbers();              //derender the numbers first

        //check for the left extreme end of the tenth digit
        if (secondNum == 0)
        {
            //the first number shouldn't be 0 for this to work normally
            if(firstNum != 0)
            {
                secondNum = 9;     //reset the second number to 9
                firstNum -= 1;     //increment the first number
            }
            //if first number is 0, then this is the minimum setting possible
            else
            {
                firstNum = 0;
                secondNum = 0;
            }
        }
        //decrement the second number if doesnt go below 0
        else
        {
            secondNum -= 1;
        }
        //keep the settings script updated
        settings.firstDigit = (float)firstNum;
        settings.secondDigit = (float)secondNum;

        //render the new numbers
        RenderNumbers();
    }
    //flip that switch
    void TogglePulse()
    {
        if(pulse)
        {
            pulse = false;
        }
        else
        {
            pulse = true;
        }
        settings.pulseActivated = pulse;        //update the settings
    }
    //flip that switch
    void ToggleFlash()
    {
        if (flash)
        {
            flash = false;
        }
        else
        {
            flash = true;
        }
        settings.flashActivated = flash;    //update the settings
    }
    //display the information based on switch state
    void DisplayPulseToggle()
    {
        if (pulse)
        {
            //don't render the empty tickbox
            pulseTickboxes[0].gameObject.SetActive(!gameObject.activeInHierarchy);
            //render the pulse tickbox
            pulseTickboxes[1].gameObject.SetActive(gameObject.activeInHierarchy);
        }
        else
        {
            //render the empty tickbox
            pulseTickboxes[0].gameObject.SetActive(gameObject.activeInHierarchy);
            //don't render the pulse tickbox
            pulseTickboxes[1].gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
    //display the information based on switch state
    void DisplayFlashToggle()
    {
        if (flash)
        {
            //don't render the empty tickbox
            flashTickboxes[0].gameObject.SetActive(!gameObject.activeInHierarchy);
            //render the flash tickbox
            flashTickboxes[1].gameObject.SetActive(gameObject.activeInHierarchy);
        }
        else
        {
            //render the empty tickbox
            flashTickboxes[0].gameObject.SetActive(gameObject.activeInHierarchy);
            //don't render the flash tickbox
            flashTickboxes[1].gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
    //display the warning sign if either pulse or flash is enabled
    void DisplayWarningSign()
    {
        if (pulse || flash)
        {
            //display warning sign if either pulse and flash is active
            WarningSign[0].gameObject.SetActive(gameObject.activeInHierarchy);
        }
        else if (!pulse || !flash)
        {
            //no need to display if neither of them are active
            WarningSign[0].gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
    //when the pulse tickbox is pressed
    public void PulseTickboxPress()
    {
        TogglePulse();
        DisplayPulseToggle();
        DisplayWarningSign();
    }
    //when the flash tickbox is pressed
    public void FlashTickboxPress()
    {
        ToggleFlash();
        DisplayFlashToggle();
        DisplayWarningSign();
    }
}
