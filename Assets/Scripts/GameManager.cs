using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using TMPro;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;
    [SerializeField]
    List<Question> questions;
    [SerializeField]
    public VRCamera VRPlayer;
    public NetworkVariableInt playersCount = new NetworkVariableInt(0);
    public NetworkVariableInt playerWin = new NetworkVariableInt(0);
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

    public bool gameStarted = false;
    public void AddPlayer()
    {
        playersCount.Value += 1;
        if(playersCount.Value >= 2)
        {
            //VRPlayer.StartGameServerRpc();
        }
    }

    void Update()
    {
        
    }

    public void Win(int playerId)
    {
        playerWin.Value = playerId;
    }
    
}
