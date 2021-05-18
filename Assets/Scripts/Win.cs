using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
     private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            VRCamera player = other.GetComponent<VRCamera>();
            GameManager.instance.Win(player.player.id);
        }
    }
}
