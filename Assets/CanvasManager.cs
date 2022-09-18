using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI boxText;
    public BoxData[] boxes;
    public BoxRotate boxRotate;
    public Animator [] anim;
    public int currentBox = 0;
    public bool boxBeingClicked;
    private void Awake()
    {
        anim = GetComponentsInChildren<Animator>(true);
        SetupBox();
    }
    private void Update()
    {

    }
    public void NextBox()
    {
        if(currentBox == boxes.Length - 1)
        {
            return;
        }
        currentBox++;
        SetupBox();
    }

    public void PreviousBox()
    {
        if (currentBox == 0)
        {
            return;
        }
        currentBox--;
        SetupBox();
    }

    public void SetupBox( )
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            if (i == currentBox)
            {
                boxes[i].gameObject.SetActive(i == currentBox);
            }
            else if (i != currentBox)
            {
                boxes[i].gameObject.SetActive(false);
            }
        }
        boxText.text = boxes[currentBox].text;
    }

    void RotateBox()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            boxRotate = GetComponentsInChildren<BoxRotate>(true)[i];
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.tag == "Box")
                {
                    boxRotate.enabled = true;
                    boxBeingClicked = true;
                }
            }
        }
    }
    public void SetupAnimator()
    {
        for (int index = 0; index < anim.Length; index++)
        {
            int animState = anim[index].GetHashCode();
            bool isCurrentBox = index == currentBox;
            if(anim[index].enabled == true && isCurrentBox)
            {
                anim[index].enabled = false;
                anim[index].StopPlayback();
            }
            else if(anim[index].enabled == false && isCurrentBox )
            {
                anim[index].enabled = true;
                anim[index].Play(animState);
            }
        }
    }

}
