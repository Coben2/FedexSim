using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class UIManager : MonoBehaviour
{
    [Header("Drag UI")]
    private Coroutine MouseRelease;
    private WaitForSeconds interval = new WaitForSeconds(5);
    public CanvasGroup clickAndDragAnimation;
    public bool boxOnScreen;
    private bool clicked;

    public Button Back;
    public Button Next;
    public bool backPressed, nextPressed;

    [Header("SceneManagement")]
    public static Button sceneProgressionButton;
    public CanvasManager cm;
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        boxOnScreen = GameObject.FindGameObjectWithTag("Box") ? true : false;
        ClickAndDrag();
    }
    
    private void ClickAndDrag()
    {
        clicked = Input.GetMouseButton(0) && BoxRotate.isDragging;
        if (Input.GetMouseButtonUp(0) && boxOnScreen)
        {
            //if mouse button is released, checks is MouseRelease has a coroutine already running and stops it before starting a new one.
            if (MouseRelease != null)
            {
                StopCoroutine(MouseRelease);
            }
            if (!clicked)
            {
                Debug.Log("Coroutine Started");
                MouseRelease = StartCoroutine(BoxRotateAndUIManipulation());
            }
        }
        else if (clicked && boxOnScreen)
        {
            StopCoroutine(MouseRelease);
            clickAndDragAnimation.alpha = 0;
            Back.gameObject.SetActive(true);
            Next.gameObject.SetActive(true);
        }
    }
    public IEnumerator BoxRotateAndUIManipulation()
    {
        yield return interval;
        Back.gameObject.SetActive( false);
        Next.gameObject.SetActive( false);
        clickAndDragAnimation.alpha = 1;
        clickAndDragAnimation.interactable = false;
    }

    public void BackButtonPress()
    {
        backPressed = true;
    }
    public void NextButtonPress()
    {
        nextPressed = true;
    }
    public bool Ready2SceneChange()
    {
        for (int i = 0; i < cm.boxes.Length; i++)
        {
            if (i == 0 && backPressed == true ) //if index is at 0 and back button pressed or index is at length and next button pressed
            {
                backPressed = false;
                return true;
            }
            else if( i == cm.boxes.Length && nextPressed == true)
            {
                nextPressed = false;
                return true;
            }
        }
        return false;
    }
}
