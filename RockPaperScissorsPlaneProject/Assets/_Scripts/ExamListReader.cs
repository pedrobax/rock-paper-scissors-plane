using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamListReader : MonoBehaviour
{
    public List<GameObject> exams; //will be used to store the exam prefabs for the entire game from the ExamList scriptable object
    public List<int> maxScore; //same as above but for max scores

    public static ExamListReader Instance;

    private void Awake()
    {
        Instance = this;
    }
}
