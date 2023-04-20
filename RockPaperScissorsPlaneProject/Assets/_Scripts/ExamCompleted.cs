using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExamCompleted : MonoBehaviour
{
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
        if (!isCoroutineRunning) StartCoroutine(CompleteExam());
    }

    private void Update()
    {
        if (!isCoroutineRunning) StartCoroutine(CompleteExam());
    }

    IEnumerator CompleteExam()
    {
        isCoroutineRunning = true;
        audioSource = GetComponent<AudioSource>();
        examScoreNumberText = examScore.GetComponent<TMP_Text>();
        overallScoreNumberText = overallScore.GetComponent<TMP_Text>();
        enemiesDefeatedNumberText = enemiesDefeated.GetComponent<TMP_Text>();
        gradeStringText = grade.GetComponent<TMP_Text>();

        examScoreNumber = PlayerPrefs.GetInt("examScore");
        overallScoreNumber = PlayerPrefs.GetInt("overallScore");
        enemiesDefeatedNumber = PlayerPrefs.GetInt("enemiesDefeated");

        examScoreNumberText.text = examScoreNumber.ToString();
        overallScoreNumberText.text = overallScoreNumber.ToString();
        enemiesDefeatedNumberText.text = enemiesDefeatedNumber.ToString();

        gradeString = CalculateGrade();
        gradeStringText.text = gradeString;

        Time.timeScale = 0.25f;
        examCompletedText.SetActive(false); examScoreText.SetActive(false); examScore.SetActive(false); overallScoreText.SetActive(false); overallScore.SetActive(false);
        enemiesDefeatedText.SetActive(false); enemiesDefeated.SetActive(false); gradeText.SetActive(false); grade.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
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
        yield return new WaitForSecondsRealtime(3f);
        examCompletedText.SetActive(false); examScoreText.SetActive(false); examScore.SetActive(false); overallScoreText.SetActive(false); overallScore.SetActive(false);
        enemiesDefeatedText.SetActive(false); enemiesDefeated.SetActive(false); gradeText.SetActive(false); grade.SetActive(false);
        GameManager.currentExam++;
        GameManager.StartExam();
        Time.timeScale = 1f;
        isCoroutineRunning = false;
        gameObject.SetActive(false);
    }

    string CalculateGrade()
    {
        float maxScore = GameManager.examList.maxScore[GameManager.currentExam];
        float score = PlayerPrefs.GetInt("examScore");
        string gradeString;

        float examGrade = score / maxScore;

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
