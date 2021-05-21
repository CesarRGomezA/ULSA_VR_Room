using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Button startHostBtn;
    [SerializeField]
    Button startClientBtn;

    void Awake() 
    {
        startHostBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
            gameObject.SetActive(false);
        });

        startClientBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
            gameObject.SetActive(false);
        });
    }
}

