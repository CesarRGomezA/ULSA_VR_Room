using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionController : MonoBehaviour
{
    Question question;
    TargetButton leftButton;
    TargetButton rightButton;
    TMP_Text questionText;

    void Awake()
    {
        leftButton = transform.Find("Panel/LeftButton").GetComponent<TargetButton>();
        rightButton = transform.Find("Panel/RightButton").GetComponent<TargetButton>();
        questionText = transform.Find("Panel/Text").GetComponent<TMP_Text>();
    }

    void Start()
    {
        FillQuestion();
    }

    void FillQuestion()
    {
        question = GameManager.instance.GetQuestion();
        questionText.text = question.Message;

        if(Random.Range(0, 2) == 0)
        {
            leftButton.transform.Find("Text").GetComponent<TMP_Text>().text = question.CorrectOption;
            leftButton.action.AddListener(() => {
                GameManager.instance.MoveForward();
            });

            rightButton.transform.Find("Text").GetComponent<TMP_Text>().text = question.IncorrectOption;
            rightButton.action.AddListener(() => {
                GameManager.instance.MoveBack();
            });
        }
        else
        {
            rightButton.transform.Find("Text").GetComponent<TMP_Text>().text = question.CorrectOption;
            rightButton.action.AddListener(() => {
                GameManager.instance.MoveForward();
            });

            leftButton.transform.Find("Text").GetComponent<TMP_Text>().text = question.IncorrectOption;
            leftButton.action.AddListener(() => {
                GameManager.instance.MoveBack();
            });
        }
    }
}
