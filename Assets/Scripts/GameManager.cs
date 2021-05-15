using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using MLAPI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform startPlayer1;
    [SerializeField]
    Transform middlePlayer1;
    [SerializeField]
    Transform lastPlayer1;
    [SerializeField]
    Transform winPlayer1;
    [SerializeField]
    Transform startPlayer2;
    public static GameManager instance;
    [SerializeField]
    List<Question> questions;

    void Awake() 
    {
        if(instance)
        {
            Destroy(gameObject);
        }
        else 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Question GetQuestion()
    {
        return questions[Random.Range(0, questions.Count)];
    }

    public void MoveForward()
    {
        Debug.Log("Forward");
    }

    public void MoveBack()
    {
        Debug.Log("Back");
    }

    public Vector3 startPointPlayer1 => startPlayer1.position;
    public Vector3 middlePointPlayer1 => middlePlayer1.position;
    public Vector3 lastPointPlayer1 => lastPlayer1.position;
    public Vector3 winPointPlayer1 => winPlayer1.position;

    public Vector3 startPointPlayer2 => startPlayer2.position;
}
