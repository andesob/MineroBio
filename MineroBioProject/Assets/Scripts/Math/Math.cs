using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

// TODO Make the code prettier. Maybe split the code into two scripts. 
public class Math : MonoBehaviour
{

    private Text questionText;
    private InputField inputField;
    private Text answerStatus;
    private Image character;

    public Sprite mainCharacterKawaii;
    public Sprite mainCharacterHmm;
    public Sprite mainCharacterCry;
    public Sprite mainCharacterAngry;

    private string currentAnswer;
    private int CorrectCounter = 0;
    private int questionNumber = 1;
    private int maxQuestions = 3;
    private int wrongAnswerCount = 0;

    private enum Status{Disabled, Active, GenereteNewEquation, HandleInput}
    private Status status;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case Status.Disabled:
                break;

            case Status.Active:
                status = Status.GenereteNewEquation;
                SelectInputField();

                break;

            case Status.HandleInput:
              
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    bool success = CheckAnswer();

                    if (success)
                    {
                        HandleSuccess();
                        StartCoroutine(SuccessTimer(3f));
                    }
                    else if (!success)
                    {
                        HandleFailure();
                        StartCoroutine(FailureTimer(3f));
                    }
                }
                break;

            case Status.GenereteNewEquation:
                if (CorrectCounter >= maxQuestions)
                {
                    SetMathUIStatusToDisabled();
                }
                else
                { 
                    GenereteEquation();
                    SelectInputField();
                    status = Status.HandleInput;
                }
                Debug.Log(currentAnswer);
                break;
        }
    }

    private void OnEnable()
    {
        questionText = GameObject.Find("TextObjects/Question").GetComponent<Text>();
        answerStatus = GameObject.Find("TextObjects/AnswerStatus").GetComponent<Text>();
        inputField = GameObject.Find("InputField").GetComponent<InputField>();

        character = GameObject.Find("Character").GetComponent<Image>();
        character.enabled = false;
        SetMathUIStatusToActive();

       
    

    }

    // Generates an equation. can be addition, subtraction, multiplication or division. 
    private void GenereteEquation()
    {
        int decider = RandomNumber(1, 100);
        if (decider > 75)
        {
            CreateAddition();
        }
        else if (decider <= 75 && decider > 50)
        {
            CreateSubtraction();
        }
        else if (decider <= 50 && decider > 25)
        {
            CreateMultiplication();
        }

        else if (decider <= 25 && decider > 0)
        {
            CreateDivision();
        }

    }
    private void CreateAddition()
    {
        int randomNumber1 = RandomNumber(1, 100);
        int randomNumber2 = RandomNumber(1, 100);
        int sum = randomNumber1 + randomNumber2;

        string nr1 = randomNumber1.ToString();
        string nr2 = randomNumber2.ToString();
        currentAnswer = sum.ToString();

        questionText.text = "Question " + questionNumber + ": " + nr1 + " + " + nr2 + " = ?" ;

    }

    // Sets the math question to a subtraction equation.
    private void CreateSubtraction()
    {
        int randomNumber1 = RandomNumber(3, 100);
        int randomNumber2 = RandomNumber(3, 100);
        if (randomNumber1 > randomNumber2)
        {
            int sum = randomNumber1 - randomNumber2;

            string nr1 = randomNumber1.ToString();
            string nr2 = randomNumber2.ToString();
            currentAnswer = sum.ToString();

            questionText.text = "Question " + questionNumber + ": " + nr1 + " - " + nr2 + " = ?";
        }
        else
        {
            int sum = randomNumber2 - randomNumber1;

            string nr1 = randomNumber1.ToString();
            string nr2 = randomNumber2.ToString();
            currentAnswer = sum.ToString();

            questionText.text = "Question " + questionNumber + ": " + nr2 + " - " + nr1 + " = ?";
        }
    

    }

    /// Sets the math question to a multiplication equation.
    private void CreateMultiplication()
    {
        int randomNumber1 = RandomNumber(1, 10);
        int randomNumber2 = RandomNumber(1, 10);
        int sum = randomNumber1 * randomNumber2;

        string nr1 = randomNumber1.ToString();
        string nr2 = randomNumber2.ToString();
        currentAnswer = sum.ToString();

        questionText.text = "Question " + questionNumber + ": " + nr1 + " * " + nr2 + " = ?";

    }


    // Sets the math question to a division equation.
    private void CreateDivision()
    {
        int decider = RandomNumber(1, 100);
        if (decider > 75)
        {
            int randomNumber1 = 5 * RandomNumber(10, 20);
            int randomNumber2 = 5;
            int sum = randomNumber1 / randomNumber2;

            string nr1 = randomNumber1.ToString();
            string nr2 = randomNumber2.ToString();
            currentAnswer = sum.ToString();

            questionText.text = "Question " + questionNumber + ": " + nr1 + " / " + nr2 + " = ?";
        }
        else if(decider <= 75 && decider > 50)
        {
            int number = RandomNumber(1, 10);
            int randomNumber1 = number * RandomNumber(2, 10);
            int randomNumber2 = number;
            int sum = randomNumber1 / randomNumber2;

            string nr1 = randomNumber1.ToString();
            string nr2 = randomNumber2.ToString();
            currentAnswer = sum.ToString();

            questionText.text = "Question " + questionNumber + ": " + nr1 + " / " + nr2 + " = ?";
        }
        else if(decider <= 50 && decider > 25)
        {
            int randomNumber1 = 10 * RandomNumber(1, 10);
            int randomNumber2 = 5 * RandomNumber(1, 2);
            int sum = randomNumber1 / randomNumber2;

            string nr1 = randomNumber1.ToString();
            string nr2 = randomNumber2.ToString();
            currentAnswer = sum.ToString();

            questionText.text = "Question " + questionNumber + ": " + nr1 + " / " + nr2 + " = ?";
        }

        else if(decider <= 25 && decider > 0)
        {
            int number =  RandomNumber(3, 10);
            int randomNumber1 = number * number;
            int randomNumber2 = number;
            int sum = randomNumber1 / randomNumber2;

            string nr1 = randomNumber1.ToString();
            string nr2 = randomNumber2.ToString();
            currentAnswer = sum.ToString();

            questionText.text = "Question " + questionNumber + ": " + nr1 + " / " + nr2 + " = ?";
        }
    }

    // Generetas a random Integer from a start range to an end range.
    private int RandomNumber(int startRange, int endRange)
    {
            int returnNumber;
            returnNumber = Random.Range(startRange, endRange);
            return returnNumber;
        
    }

    // Sets the pointer to the input field. 
    private void SelectInputField()
    {
        Debug.Log("select input called");
        inputField.Select();
        inputField.ActivateInputField();
    }

    // Return true if the value of the input field is equal to the value of the current answer.
    private bool CheckAnswer()
    {
        bool correctAnswer = false;
        string answer = GetAnswer();
        if (answer.Equals(currentAnswer)){
            correctAnswer = true;
        }
        return correctAnswer;
    }

    // Returns the math answer from the input field. 
    private string GetAnswer()
    {
        return inputField.text.ToString();
    }

    // Sets the MathUI object to active, and pauses the game.
    // TODO Might want to move this method. Depends on the object that calls it.
    public void SetMathUIStatusToActive()
    {
        Time.timeScale = 0f; 
        PlayerController.isGamePaused = true;
        //this.gameObject.SetActive(true);
        status = Status.Active;
        
    }

    // Sets the MathUI object to disabled and starts the game again.
    // TODO Might want to move this method. Depends on the object that calls it.
    public void SetMathUIStatusToDisabled()
    {
       Time.timeScale = 1f;
        PlayerController.isGamePaused = false;
        this.gameObject.SetActive(false);
        status = Status.Disabled;
    }

    //Sets the time for how long the UI elements says the correct.
    private IEnumerator SuccessTimer(float timout)
    {
        inputField.enabled = false;
        character.enabled = true;
        yield return new WaitForSecondsRealtime(timout);
        inputField.enabled = true;
        ClearInputField();
        ClearStatusField();
        status = Status.GenereteNewEquation;
        character.enabled = false;
    }

    //Sets the time for how long the UI elements says the correct.
    private IEnumerator FailureTimer(float timout)
    {
        inputField.enabled = false;
        character.enabled = true;
        yield return new WaitForSecondsRealtime(timout);
        inputField.enabled = true;
        ClearInputField();
        ClearStatusField();
        SelectInputField();
        character.enabled = false;
        
    }

    private void HandleSuccess()
    {
        answerStatus.text = "Correct!";
        character.sprite = mainCharacterKawaii;
        CorrectCounter++;
        questionNumber++;
        wrongAnswerCount = 0;
    }

    private void HandleFailure()
    {
        character.enabled = true;
        answerStatus.text = "Wrong answer";
        wrongAnswerCount++;
        if(wrongAnswerCount == 1)
        {
            character.sprite = mainCharacterHmm;
        }
        else if(wrongAnswerCount == 2)
        {
            character.sprite = mainCharacterAngry;
        }
        else if (wrongAnswerCount >= 3)
        {
            character.sprite = mainCharacterCry;
        }
    }

    private void ClearInputField()
    {
        inputField.text = "";
    }

    private void ClearStatusField()
    {
        answerStatus.text = "";
    }
}

