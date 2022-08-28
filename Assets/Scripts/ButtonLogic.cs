using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;
public class ButtonLogic : MonoBehaviour
{
    public ButtonScreen[] buttons;
    public int currentButton;
    public BoxRotate[] boxRotates;
    private Dictionary<int, Transform> camPlacement = new Dictionary<int, Transform>();

    private Camera cam;
    
    private void Start()
    {
        cam = Camera.main;

        camPlacement.Add(0, boxRotates[0].transform); //only back[0] should reach this value
        camPlacement.Add(1, boxRotates[1].transform); //only next[0] and back[1] should reach this value
        //camPlacement.Add(2, boxRotates[2].transform); //only next[1] and back[2] should reach this value
        //camPlacement.Add(3, boxRotates[3].transform); //only next[2] should reach this value
    }
    public void SetButton()
    {
        if(buttons!= null)
        {
            currentButton = buttons.;
        }
        else
        {
            Debug.LogError("Must make more buttons");
        }

    }
    public void NextButton(int button)
    {
        foreach(var b in buttons)
        {
            int buttonIndex = b.buttonOrder;
            if ( button == currentButton)
            {
                b.activated = false;
                b.buttonsList[buttonIndex].gameObject.SetActive(b.activated); //deactivates the current button when clicked
                var nextButton = buttons[b.buttonOrder+1];
                nextButton.activated = true;
                b.buttonsList[buttonIndex + 1].gameObject.SetActive(nextButton.activated); //activates the next button when clicked

                currentButton = nextButton.buttonOrder;

                CamPlacements(buttonIndex);
            }
            if (boxRotates[buttonIndex + 1] != null)
            {
                boxRotates[buttonIndex + 1].enabled = true;
                boxRotates[buttonIndex].enabled = false;
            }
        }

    }
    public void BackButton(int button)
    {
        foreach (var b in buttons)
        {
            int buttonIndex = b.buttonOrder;
            if (button == currentButton)
            {
                b.activated = false;
                b.buttonsList[buttonIndex].gameObject.SetActive(b.activated); //deactivates the current button when clicked
                var backButton = buttons[b.buttonOrder - 1];
                backButton.activated = true;
                b.buttonsList[buttonIndex - 1].gameObject.SetActive(backButton.activated); //activates the next button when clicked

                currentButton = backButton.buttonOrder;

                CamPlacements(buttonIndex);
            }
            if (boxRotates[buttonIndex - 1] != null)
            {
                boxRotates[buttonIndex - 1].enabled = true;
                boxRotates[buttonIndex].enabled = false;
            }
        }
    }
    private void CamPlacements(int buttonIndex)
    {
        foreach (var b in camPlacement)
        {
            //is dictionary of a dictionary necessary to get the index of button and button itself?
            var currentCamPos = b;
            int camIndex = currentCamPos.Key;
            if(camIndex == currentButton)
            {
                var newCamPosition = camPlacement[buttonIndex]; //Wanted to do camPlacement[b +1] for unity button event 
                cam.transform.position = newCamPosition.transform.position;
            }
            camIndex++;
        }

    }
}
