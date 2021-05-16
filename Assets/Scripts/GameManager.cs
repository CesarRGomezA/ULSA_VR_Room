using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using MLAPI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    Transform[] player1Positions;
    int player1CurrentPos = 0;
    int player2CurrentPos = 0;
    [SerializeField]
    
    Transform[] player2Positions;    
    [SerializeField]
    List<Question> questions;
    [SerializeField]
    float moveSpeed;

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

    public void GetPlayerPosition(VRCamera player)
    {
        if(player.player == 1)
        {
            player.transform.position = player1Positions[player1CurrentPos].position;
        }
        else
        {
            player.transform.position = player2Positions[player2CurrentPos].position;
        }
    }

    public void MoveToNextStep(VRCamera player)
    {
        if(player.player == 1)
        {
            Debug.Log(1);
            StartCoroutine(MoveToPoint(player, player1Positions[++player1CurrentPos].position, moveSpeed));
        }
        else
        {
            StartCoroutine(MoveToPoint(player, player2Positions[++player2CurrentPos].position, moveSpeed));
        }
    }

    IEnumerator MoveToPoint(VRCamera player, Vector3 target, float speed)
    {
        while (Vector3.Distance(player.transform.position, target) > 0.1f)
        {
            float step = speed * Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, step);
            yield return null;
        }
    }
}
