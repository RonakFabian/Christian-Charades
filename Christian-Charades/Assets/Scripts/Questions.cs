using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questions : MonoBehaviour
{
    public List<string> questions;

    public int index = -1;

    public string GetNextQuestion()
    {
        if (index < questions.Count)
        {
            return questions[index++];


        }
        else
        {
            return "Round Over!";
        }

    }

    public void IncreaseIndex()
    {
        if (index < questions.Count)
            index++;
    }
}
