using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamListReader : MonoBehaviour
{
    public List<GameObject> exams;
    public List<int> maxScore;

    public static ExamListReader Instance;

    private void Awake()
    {
        Instance = this;
    }
}
