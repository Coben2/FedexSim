using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boxclick : MonoBehaviour //SCRIPTABLE OBJECT FOR THE BOXES????????????????????????????????????????????????????????????
{
    // Start is called before the first frame update
    private Camera mainCam;
    private Ray ray;
    public BoxCollider parentCollider;
    public Transform openBox;
    public Transform boxCluster;
    public Transform firstBox;

    private Dictionary<ArrayList, Vector3> camPlacement = new Dictionary<ArrayList, Vector3>();
    private Dictionary<int, Button> buttonPlacement = new Dictionary<int, Button>(); //how to make buttons both back and next buttons as the value (New Dictionary?)
    public Vector3[] camPositions;
    public Button beginningBackButton;
    public Button[] backButton = new Button[3]; //TODO: Rearrange Back Buttons 
    public Button[] nextButton = new Button[3];
    public BoxRotate[] boxRotates;
    public Buttons buttons;
    //public List<Buttons> buttonsList;

    public CanvasGroup ICGroup; 
    [SerializeField] private BoxRotate boxRotate; //TODO: Delete refs of this to include in array
    public TextMeshProUGUI clickBoxPrompt;
    public CanvasGroup backButtonPrompt;
    public CanvasGroup clickAndDragAnimation;
    public Canvas boxInfo;

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

        //buttonsList = new List<Buttons>(buttons.buttonsList.Count);

        //camPlacement.Add(buttons.b, camPositions[0]);
        //camPlacement.Add(backButton[1], camPositions[1]);
        //camPlacement.Add(backButton[2], camPositions[2]);
        //camPlacement.Add(backButton[3], camPositions[3]);

        buttonPlacement.Add(1, backButton[1]);
        buttonPlacement.Add(2, backButton[2]);
        buttonPlacement.Add(3, backButton[3]);
        buttonPlacement.Add(4, backButton[4]);
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

       
        CamPlacements();

        

    }

    public void NextButton(int button)
    {
        foreach(var b in buttonPlacement)
        {
            //TODO: Check if any buttons or box rotates are null
            button = b.Key;
            if(buttonPlacement[b.Key +1] != null)
            {
                buttonPlacement[button + 1].enabled = true;
                buttonPlacement[button].enabled = false;
            }
            if(boxRotates[b.Key + 1] != null)
            {
                boxRotates[b.Key + 1].enabled = true;
                boxRotates[b.Key].enabled = false; //is this good for enabling the next box's rotation?
            }
        }
        CamPlacements();
    }
    public void BackButton(int button)
    {
        foreach(var b in buttonPlacement)
        {

            button = b.Key;
            if(buttonPlacement[b.Key -1] != null)
            {
                buttonPlacement[button - 1].enabled = true;
                buttonPlacement[button].enabled = false;
            }
            if(boxRotates[b.Key -1] != null)
            {
                boxRotates[b.Key - 1].enabled = true;
                boxRotates[b.Key].enabled = false;
            }
        }
        CamPlacements();
    }
    private void CamPlacements()
    {
       foreach (var b in camPlacement)
        {
            //is dictionary of a dictionary necessary to get the index of button and button itself?
            var newCamPosition = camPlacement[b.Key]; //Wanted to do camPlacement[b +1] for unity button event 
            mainCam.transform.position = newCamPosition;

            
            
        }
        
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
 