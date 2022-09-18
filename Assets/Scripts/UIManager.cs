using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Coroutine MouseRelease;
    private WaitForSeconds interval = new WaitForSeconds(5);
    public Button Back;
    public Button Next;
    public CanvasGroup clickAndDragAnimation;
    public CanvasManager cm;
    public bool boxOnScreen;
    // Start is called before the first frame update
    void Awake()
    {
        cm = FindObjectOfType<CanvasManager>();
    }
    private void Start()
    {
        boxOnScreen = GameObject.FindWithTag("Box") ? true : false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && boxOnScreen)
        {
            //if mouse button is released, checks is MouseRelease has a coroutine already running and stops it before starting a new one.
            if (MouseRelease != null)
            {
                StopCoroutine(MouseRelease);
            }
            if (!cm.boxBeingClicked)
            {
                Debug.Log("Coroutine Started");
                MouseRelease = StartCoroutine(BoxRotateAndUIManipulation());
            }
        }
        else if(Input.GetMouseButtonDown(0) && boxOnScreen)
        {
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
}
