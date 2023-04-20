using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExamList", menuName = "Exam/ExamList", order = 1)]
public class ExamList : ScriptableObject
{
    public List<GameObject> exams;
    public List<int> maxScore;
}
