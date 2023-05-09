using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExamList", menuName = "Exam/ExamList", order = 1)]
//object is used to score Exam prefabs, these are the "levels" inside each level of the game
public class ExamList : ScriptableObject
{
    public List<GameObject> exams; //list of exam prefabs fo the entire game
    public List<int> maxScore; //list of the max score for each exam in the same index on the above list
}
