using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using label;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject box;
    public GameObject boxCopy;
    public Transform boxStartPoint;
    public Transform boxEndPoint;
    [Header("UI Graphics")]
    [SerializeField] private Canvas doorOpen;
    [SerializeField] private Canvas questionCanvas;
    [SerializeField] public TMP_Text dimensions;
    [SerializeField] public TMP_Text weight;
    [SerializeField] public TMP_Text questions;
    public Conveyor conveyor;

    public List<Label> boxArray = new List<Label>();
    Label currentLabel;

    [Header("Buttons")]
    public Button yesButton;
    public Button noButton;

    public int previousRandomValue = -1;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    public void CoroutineStart()
    {
        //Starts Box Move(configurable with button)
        StartCoroutine(BoxMove());
    }
    public IEnumerator BoxMove()
    {
            //starts a timer for box to move
            float timer = 0;
            boxCopy =  Instantiate(box, boxStartPoint);
            //sets box position between zero and one second
            while(timer < 1)
            {
               
                timer += Time.deltaTime;
                boxCopy.transform.position = Vector3.Lerp(boxStartPoint.position, boxEndPoint.position, timer);
                yield return new WaitForEndOfFrame();
            }
        NewQuestion();
        
        
    }

    private void NewQuestion()
    {
        Debug.Log("UI can show now");
        questionCanvas.gameObject.SetActive(true);
        questions.text = ("Does this box go on the belt?");
        currentLabel = boxArray[GetRandomValue()];
        weight.text = "Weight: " + currentLabel.weight.ToString() + " pounds";
        dimensions.text = ("Height: " + currentLabel.height.ToString() + "Width: " + currentLabel.width.ToString() + "Length: " + currentLabel.length.ToString());
    }

    private int GetRandomValue()
    {
        //gets a random value from list
        int random = UnityEngine.Random.Range(0, boxArray.Count);
        
        if(random == previousRandomValue)
        {
           GetRandomValue();
            return random;
        }
        else
        {
            previousRandomValue = random;
            return random;
        }
    }

    public void IsAnswerCorrect()
    {
     
        bool isWeightCorrect;
        bool isHeightCorrect;
        if(currentLabel!= null)
        {
            isWeightCorrect = currentLabel.weight <= 75 ? true : false;
            isHeightCorrect = currentLabel.height <= 59 ? true : false;
            if (isHeightCorrect || isWeightCorrect)
            {
                StartCoroutine(CorrectAnswer());
            }
            else
            {
               // StartCoroutine(IncorrectAnswer());
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
      
    }


    public IEnumerator CorrectAnswer()
    {
        questions.text = ("CORRECT!");
        questionCanvas.gameObject.SetActive(false);
        conveyor.MoveBox();
        yield return new WaitForSeconds(.1f);
         NewQuestion();
         
       
    }

    public IEnumerator IncorrectAnswer()
    {
        questions.text = ("INCORRECT!");
        yield return new WaitForSeconds(.1f);
        boxCopy.SetActive(false);


        

        BoxMove();



    }
}
