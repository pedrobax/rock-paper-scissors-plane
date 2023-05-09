using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExamCompleted : MonoBehaviour
{
    //game objects that store different texts in the UI
    [SerializeField] GameObject examCompletedText;
    [SerializeField] GameObject examScoreText;
    [SerializeField] GameObject examScore;
    [SerializeField] GameObject overallScoreText;
    [SerializeField] GameObject overallScore;
    [SerializeField] GameObject enemiesDefeatedText;
    [SerializeField] GameObject enemiesDefeated;
    [SerializeField] GameObject gradeText;
    [SerializeField] GameObject grade;

    AudioSource audioSource;

    public int examScoreNumber;
    public int overallScoreNumber;
    public int enemiesDefeatedNumber;
    public string gradeString = "C+";

    TMP_Text examScoreNumberText;
    TMP_Text overallScoreNumberText;
    TMP_Text enemiesDefeatedNumberText;
    TMP_Text gradeStringText;

    bool isCoroutineRunning = false;

    private void Start()
    {
        //the exam completed UI is only shown when the exam is completed, so it is disabled at the start of the game
        //the screen is set active by game manager when it is needed
        if (!isCoroutineRunning) StartCoroutine(CompleteExam());
    }

    private void Update()
    {
        //also here so it can work multiple times in single scene session
        if (!isCoroutineRunning) StartCoroutine(CompleteExam());
    }

    IEnumerator CompleteExam()
    {
        isCoroutineRunning = true; //sets coroutine to running so it can't be called again

        //gets necessary components
        audioSource = GetComponent<AudioSource>();
        examScoreNumberText = examScore.GetComponent<TMP_Text>();
        overallScoreNumberText = overallScore.GetComponent<TMP_Text>();
        enemiesDefeatedNumberText = enemiesDefeated.GetComponent<TMP_Text>();
        gradeStringText = grade.GetComponent<TMP_Text>();

        //gets values from player prefs
        examScoreNumber = PlayerPrefs.GetInt("examScore");
        overallScoreNumber = PlayerPrefs.GetInt("overallScore");
        enemiesDefeatedNumber = PlayerPrefs.GetInt("enemiesDefeated");

        //sets texts gathered from player prefs
        examScoreNumberText.text = examScoreNumber.ToString();
        overallScoreNumberText.text = overallScoreNumber.ToString();
        enemiesDefeatedNumberText.text = enemiesDefeatedNumber.ToString();

        //calculates grade based on score
        gradeString = CalculateGrade();
        gradeStringText.text = gradeString; //modifies text based on grade

        Time.timeScale = 0.25f; //slow-mo effect
        //sets everything to false so it can be enabled one by one, causing the "stamp" effect
        examCompletedText.SetActive(false); examScoreText.SetActive(false); examScore.SetActive(false); overallScoreText.SetActive(false); overallScore.SetActive(false);
        enemiesDefeatedText.SetActive(false); enemiesDefeated.SetActive(false); gradeText.SetActive(false); grade.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        //enables everything one by one, causing the "stamp" effect, with time delays between each
        examCompletedText.SetActive(true);
        audioSource.pitch = 1;
        audioSource.Play();
        audioSource.pitch += .1f;
        yield return new WaitForSecondsRealtime(2f);
        examScoreText.SetActive(true);
        audioSource.Play();
        audioSource.pitch += .1f;
        yield return new WaitForSecondsRealtime(1f);
        examScore.SetActive(true);
        audioSource.Play();
        audioSource.pitch += .1f;
        yield return new WaitForSecondsRealtime(1f);
        overallScoreText.SetActive(true);
        audioSource.Play();
        audioSource.pitch += .1f;
        yield return new WaitForSecondsRealtime(1f);
        overallScore.SetActive(true);
        audioSource.Play();
        audioSource.pitch += .1f;
        yield return new WaitForSecondsRealtime(1f);
        enemiesDefeatedText.SetActive(true);
        audioSource.Play();
        audioSource.pitch += .1f;
        yield return new WaitForSecondsRealtime(1f);
        enemiesDefeated.SetActive(true);
        audioSource.Play();
        audioSource.pitch += .1f;
        yield return new WaitForSecondsRealtime(1f);
        gradeText.SetActive(true);
        audioSource.Play();
        audioSource.pitch += .1f;
        yield return new WaitForSecondsRealtime(2f);
        grade.SetActive(true);
        audioSource.Play();
        audioSource.pitch += .1f;
        yield return new WaitForSecondsRealtime(3f); //gives player enough time to read everything before turning it all off
        examCompletedText.SetActive(false); examScoreText.SetActive(false); examScore.SetActive(false); overallScoreText.SetActive(false); overallScore.SetActive(false);
        enemiesDefeatedText.SetActive(false); enemiesDefeated.SetActive(false); gradeText.SetActive(false); grade.SetActive(false);
        GameManager.currentExam++; //informs GameManager to move to next exam
        GameManager.StartExam(); //starts next exam in GameManager
        Time.timeScale = 1f; //stops slow-mo effect
        isCoroutineRunning = false; //sets coroutine to not running so it can be called again
        gameObject.SetActive(false); //hides the ExamCompleted UI so it only shows up again when needed
    }

    //calculates the grade and returns the string to display on screen
    string CalculateGrade()
    {
        float maxScore = GameManager.examList.maxScore[GameManager.currentExam]; //gets max score for current exam from exam list
        float score = PlayerPrefs.GetInt("examScore"); //gets score from Player Prefs
        string gradeString; //string that will be modified and used to return

        //stores score percentage based on max score
        float examGrade = score / maxScore;

        //sets string grade based on score percentage
        switch (examGrade)
        {
            case >= 0.99f:
                gradeString = "S+";
                break;
            case >= 0.95f:
                gradeString = "S";
                break;
            case >= 0.90f:
                gradeString = "A+";
                break;
            case >= 0.85f:
                gradeString = "A";
                break;
            case >= 0.80f:
                gradeString = "B+";
                break;
            case >= 0.75f:
                gradeString = "B";
                break;
            case >= 0.70f:
                gradeString = "C+";
                break;
            case >= 0.65f:
                gradeString = "C";
                break;
            case >= 0.60f:
                gradeString = "D+";
                break;
            case >= 0.55f:
                gradeString = "D";
                break;
            case >= 0.50f:
                gradeString = "E+";
                break;
            default:
                gradeString = "E";
                break;
        }
        return gradeString;
    }
}
