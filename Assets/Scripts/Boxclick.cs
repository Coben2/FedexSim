using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boxclick : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera mainCam;
    private Ray ray;
    public BoxCollider parentCollider;
    public Transform openBox;
    public Transform boxCluster;
    public Transform firstBox;

    public CanvasGroup ICGroup;
    [SerializeField] private BoxRotate boxRotate;
    public TextMeshProUGUI clickBoxPrompt;
    public CanvasGroup backButtonPrompt;
    public CanvasGroup clickAndDragAnimation;
    public Canvas boxInfo;
    public Button[] backButton = new Button[3];
    public Button[] nextButton = new Button[3];
    public Button previousSceneButton;
    public Button startButton;

    private int zoomParameterHash;
    private WaitForSeconds interval = new WaitForSeconds(5);

    private bool isRotating;

    [SerializeField] private Animator boxAnimation;
    void Start()
    {
        zoomParameterHash = Animator.StringToHash("Zoom");
        boxInfo.gameObject.SetActive(false);
        boxRotate.enabled = false;
        mainCam = Camera.main;
        backButtonPrompt.alpha = 0;
        clickAndDragAnimation.alpha = 0;
        boxAnimation.SetBool("Bob", true);
        clickBoxPrompt.enabled = true;
        
    }
    private Coroutine MouseRelease;

    // Update is called once per frame
    void Update()
    {
        bool clicked = Input.GetKey(KeyCode.Mouse0);
        if (clicked)
        {
  
            ray = mainCam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            // disable parents box collider
            parentCollider.enabled = false;


            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.gameObject.tag == "Player")
                {
                    backButtonPrompt.alpha = 1;
                    //allow box to rotate
                    boxRotate.enabled = true;
                    boxInfo.gameObject.SetActive(true);
                    isRotating = true;
                    clickAndDragAnimation.alpha = 0;
                    clickAndDragAnimation.interactable = false;

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

    public void StartButton()
    {
        //stop bobbing animation
        boxAnimation.SetBool("Bob", false);
        boxAnimation.SetTrigger(zoomParameterHash);

        //activate box stats text
        clickBoxPrompt.enabled = false;
        backButtonPrompt.alpha = 1;
        backButtonPrompt.interactable = true;
        previousSceneButton.gameObject.SetActive(false);
        //allow box to rotate
        boxRotate.enabled = true;
        boxInfo.gameObject.SetActive(true);
        isRotating = true;
        clickAndDragAnimation.alpha = 0;
        clickAndDragAnimation.interactable = false;
    }
    public IEnumerator BoxRotateAndUIManipulation()
    {
        yield return interval;
        backButtonPrompt.alpha = 0;
        clickAndDragAnimation.alpha = 1;
        clickAndDragAnimation.interactable = false;

    }
    public void BackButton0()
    {
        parentCollider.enabled = true;
        boxAnimation.SetBool("Bob", true);
        backButtonPrompt.alpha = 0;
        backButtonPrompt.interactable = false;
        clickBoxPrompt.enabled = true;
        previousSceneButton.gameObject.SetActive(true);

        this.transform.rotation = Quaternion.identity;
        boxRotate.turn = Vector2.zero;
        boxRotate.enabled = false;
        boxInfo.gameObject.SetActive(false);

    }

    public void BackButton1()
    {
        clickBoxPrompt.enabled = false;
        backButtonPrompt.alpha = 1;
        backButtonPrompt.interactable = true;
        previousSceneButton.gameObject.SetActive(false);
        clickAndDragAnimation.alpha = 0;
        clickAndDragAnimation.interactable = false;
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, firstBox.position, 1);
    }
    
    public void BackButton2()
    {
        clickBoxPrompt.enabled = false;
        backButtonPrompt.alpha = 1;
        backButtonPrompt.interactable = true;
        previousSceneButton.gameObject.SetActive(false);
        clickAndDragAnimation.alpha = 0;
        clickAndDragAnimation.interactable = false;
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, new Vector3(6, .5f, -2f), 1);
    }


    public void BackButton3()
    {
        clickBoxPrompt.enabled = false;
        backButtonPrompt.alpha = 1;
        backButtonPrompt.interactable = true;
        previousSceneButton.gameObject.SetActive(false);
        clickAndDragAnimation.alpha = 0;
        clickAndDragAnimation.interactable = false;
        Vector3 boxClusterCamViewPos = new Vector3(13, .86f, -3.45f);
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, boxClusterCamViewPos, 1);
        ICGroup.alpha = 0;

    }
    public void NextBox0()
    {
        backButton[0].gameObject.SetActive(false);
        backButton[1].gameObject.SetActive(true);
        Vector3 openBoxCamViewPos = new Vector3(6, .5f, -2f);
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, openBoxCamViewPos, 1);
        var openBoxRot = openBox.GetComponent<BoxRotate>();
        openBoxRot.enabled = true;
    }

    public void NextBox1()
    {
        backButton[1].gameObject.SetActive(false);
        backButton[2].gameObject.SetActive(true);
        Vector3 boxClusterCamViewPos = new Vector3(13, .86f, -3.45f);
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, boxClusterCamViewPos, 1);
        var boxClusterRot = boxCluster.GetComponent<BoxRotate>();
        boxClusterRot.enabled = true;
    }

    public void ICScenePrompt()
    {
        backButtonPrompt.alpha = 0;
        ICGroup.alpha = 1;
    }


}
 