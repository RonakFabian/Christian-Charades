using System.Collections;
using TMPro;
using UnityEngine;

public class PhoneFlipDetector : MonoBehaviour
{
    private bool isGyroEnabled;

    private enum PhoneState { FacingUp, FacingDown, FlippedUp, FlippedDown, Neutral }
    private PhoneState currentState = PhoneState.Neutral;
    public TextMeshProUGUI txt;

    Vector3 orientation;
    PhoneState newState;

    public Questions questions;

    bool canStartNextQuestion;
    bool begin = false;
    public float TimeLeft;
    public bool TimerOn = false;
    public TextMeshProUGUI TimerTxt;

    public Color32 neutral;
    public Color32 pass;
    public Color32 correct;

    public UnityEngine.UI.Image img;

    void Start()
    {

        img.color = neutral;
        txt.text = "";
        TimerTxt.text = "";
        canStartNextQuestion = false;
        StartCoroutine("StartGameState");
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        // Check if the device has a gyroscope
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true; // Enable the gyroscope
            isGyroEnabled = true;
        }

    }



    void Update()
    {


        orientation = Input.gyro.gravity;

        // Detect current phone state based on orientation

        if (canStartNextQuestion && begin)
        {
            newState = GetPhoneState(orientation);

            // Check for state change
            if (newState != currentState)
            {
                HandleStateChange(newState);
                currentState = newState;
            }
        }


        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                Debug.Log("Time is UP!");
                TimeLeft = 0;
                TimerOn = false;
            }
        }
    }

    private PhoneState GetPhoneState(Vector3 orientation)
    {
        if (orientation.z > 0.6f)
        {
            return PhoneState.FacingUp;
        }
        else if (orientation.z < -0.8f)
        {
            return PhoneState.FacingDown;
        }
        // else if (orientation.y > 0.8f)
        // {
        //     return PhoneState.FlippedUp;
        // }
        // else if (orientation.y < -0.8f)
        // {
        //     return PhoneState.FlippedDown;
        // }
        else
        {
            return PhoneState.Neutral;
        }
    }

    private void HandleStateChange(PhoneState newState)
    {
        switch (newState)
        {
            case PhoneState.FacingUp:
                Debug.Log("Phone is now facing up.");

                StartCoroutine("CorrectState");


                break;
            case PhoneState.FacingDown:
                Debug.Log("Phone is now facing down.");
                StartCoroutine("PassState");

                break;

            case PhoneState.Neutral:
                Debug.Log("Phone is in a neutral position.");
                canStartNextQuestion = true;
                StartCoroutine("LoadNextquestion");
                Neutral();
                break;
        }
    }

    IEnumerator LoadNextquestion()
    {
        string question = questions.GetNextQuestion();
        txt.text = question;
        if (question == "Round Over!")
        {

            TimerOn = false;

        }

        canStartNextQuestion = false;
        yield return new WaitForSecondsRealtime(0.1f);
        canStartNextQuestion = true;
        if (question == "Round Over!")
        {
            canStartNextQuestion = false;

        }
        else
            canStartNextQuestion = true;


    }

    private void Neutral()
    {
        img.color = neutral;


    }


    IEnumerator CorrectState()
    {
        img.color = correct;

        Handheld.Vibrate();
        txt.text = "Correct!";
        canStartNextQuestion = false;
        yield return new WaitForSecondsRealtime(2);
        canStartNextQuestion = true;
    }



    IEnumerator PassState()
    {
        img.color = pass;

        Handheld.Vibrate();
        txt.text = "Pass";
        canStartNextQuestion = false;
        yield return new WaitForSecondsRealtime(2);
        canStartNextQuestion = true;

    }
    IEnumerator StartGameState()
    {
        txt.text = "Get Ready";
        Handheld.Vibrate();

        yield return new WaitForSecondsRealtime(1);
        txt.text = "3";


        yield return new WaitForSecondsRealtime(1);
        txt.text = "2";


        yield return new WaitForSecondsRealtime(1);
        txt.text = "1";


        yield return new WaitForSecondsRealtime(1);
        Handheld.Vibrate();

        canStartNextQuestion = true;
        begin = true;
        TimerOn = true;
        txt.text = questions.GetNextQuestion();



    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
