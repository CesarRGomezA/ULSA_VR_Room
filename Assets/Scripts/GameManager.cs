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

    public int playersCount = 0;
    public List<VRCamera> players = new List<VRCamera>();
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
    public void AddPlayer(VRCamera player)
    {
        player.id = players.Count;
        players.Add(player);
        if(players.Count >= 3)
        {
            foreach(VRCamera p in players)
            {
                p.StartGame();
            }   
        }
    }

    void Update()
    {
        
    } 
}
