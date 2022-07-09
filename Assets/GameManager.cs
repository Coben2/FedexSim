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
    [SerializeField] private Canvas gameFinishedUI;
    [SerializeField] public TMP_Text dimensions;
    [SerializeField] public TMP_Text weight;
    [SerializeField] public TMP_Text questions;
    [SerializeField] public TMP_Text correctAnswersText;
    public Conveyor conveyor;

    public bool correctBox;

    public List<Label> boxArray = new List<Label>();
    Label currentLabel;

    private int numOfQuestions;
    private int currentQuestion;
    private int possibleCorrectAnswers;

    [Header("Buttons")]
    public Button yesButton;
    public Button noButton;

    public int previousRandomValue = -1;
    // Start is called before the first frame update
    void Start()
    {
        correctBox = false;
        numOfQuestions = 3;
        currentQuestion = 0;
        possibleCorrectAnswers = numOfQuestions;
    }

    public void CoroutineStart()
    {
        //Starts Box Move(configurable with button)
        StartCoroutine(BoxInitialize());
    }
    public IEnumerator BoxInitialize()
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
        NewQuestion(boxCopy);
        
    }

    private void NewQuestion(GameObject boxCopy)
    {
        Debug.Log("UI can show now");
        questionCanvas.gameObject.SetActive(true);
        questions.text = ("Does this box go on the belt?");
        currentLabel = boxArray[GetRandomValue()];
        weight.text = "Weight: " + currentLabel.weight.ToString() + " pounds";
        dimensions.text = ("Height: " + currentLabel.height.ToString() + "Width: " + currentLabel.width.ToString() + "Length: " + currentLabel.length.ToString());
        boxCopy.transform.localScale = new Vector3((currentLabel.height * .01f + currentLabel.width * .01f)/2, currentLabel.height * .01f, currentLabel.width * .01f);
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

    public void IsAnswerCorrect(bool clickedYes)
    {
        currentQuestion ++;
        bool isWeightCorrect;
        bool isHeightCorrect;
        isWeightCorrect = currentLabel.weight <= 75 ? true : false;
        isHeightCorrect = currentLabel.height <= 59 ? true : false;
        bool answeredWrong = !clickedYes && (isHeightCorrect || isWeightCorrect);
        bool answeredRight = clickedYes && (isHeightCorrect || isWeightCorrect);
        if (currentLabel!= null)
        {
           if(answeredRight)
            {
                StartCoroutine(CorrectAnswer());
                Debug.Log("Correct");
            }
            if(answeredWrong)
            {
                possibleCorrectAnswers--;
                StartCoroutine(IncorrectAnswer());
                Debug.Log("Incorrect");
            }
        }
        if (currentQuestion >= numOfQuestions)
        {
            questionCanvas.enabled = false;
            gameFinishedUI.gameObject.SetActive(true);
            correctAnswersText.text = "Congratulations! You got " + possibleCorrectAnswers + "/" + numOfQuestions + " right!";
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

        correctBox = true;

        yield return new WaitUntil(() => conveyor.CheckDistance());

        yield return BoxInitialize();
       
    }

    public IEnumerator IncorrectAnswer()
    {
        correctBox = false;
        questions.text = ("INCORRECT!");
        boxCopy.SetActive(false);
        return  BoxInitialize();



    }
}
