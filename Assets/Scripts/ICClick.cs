using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICClick : MonoBehaviour
{
    private Camera mainCam;
    private Ray ray;
    private bool isRotating;
    public GameObject clickAndDragAnimation;
    private Coroutine MouseRelease;
    private WaitForSeconds interval = new WaitForSeconds(5);
    [SerializeField] private BoxRotate boxRotate;
    [SerializeField] private Canvas icInfo;

    // Start is called before the first frame update
    void Start()
    {
        if(icInfo.isActiveAndEnabled)
        {
            icInfo.gameObject.SetActive(false);
        }
      
        mainCam = Camera.main;
        clickAndDragAnimation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool clicked = Input.GetKey(KeyCode.Mouse0);
        if (clicked)
        {

            ray = mainCam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.gameObject.tag == "Player")
                {

                    boxRotate.enabled = true;
                    isRotating = true;
                    clickAndDragAnimation.SetActive(false);

                }

            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Up");
            //if mouse button is released, checks is MouseRelease has a coroutine already running and stops it before starting a new one.
            if (MouseRelease != null)
            {
                StopCoroutine(MouseRelease);
            }
            isRotating = false;
            if (isRotating == false)
            {
                MouseRelease = StartCoroutine(BoxRotateAndUIManipulation());
                Debug.Log("Coroutine Started");
            }

        }
    }

    public IEnumerator BoxRotateAndUIManipulation()
    {
        yield return interval;
        clickAndDragAnimation.SetActive(true);
    }
}
