using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Player : NetworkBehaviour
{
    [SerializeField]
    Transform[] Positions;
    int CurrentPos = 0;
    public VRCamera VRPlayer;
    [SerializeField]
    GameObject[] questions;
    [SerializeField]
    float moveSpeed = 3;
    [SerializeField]
    GameObject bridges;
    public int id;
    
    public void StartPlayer()
    {
        VRPlayer.transform.position = Positions[CurrentPos].position;
    }

    public void MoveToNextStep()
    {       
        StopCoroutine("MoveToPoint");
        StartCoroutine(MoveToPoint(VRPlayer, Positions[++CurrentPos].position, moveSpeed));
    }

    public void MoveBack()
    {
        bridges.SetActive(false);
    }

    public void ResetPlayer()
    {
        bridges.SetActive(true);
        CurrentPos = 0;
        VRPlayer.transform.rotation = Quaternion.Euler(new Vector3(0f,-90f,0f));
        VRPlayer.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f); 
        VRPlayer.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f,0f,0f); 
        VRPlayer.transform.position = Positions[CurrentPos].position;

        MoveToNextStep();
        foreach(GameObject question in questions)
        {
            question.GetComponent<QuestionController>().FillQuestion();
        }
    }

    IEnumerator MoveToPoint(VRCamera VRPlayer, Vector3 target, float speed)
    {
        while (Vector3.Distance(VRPlayer.transform.position, target) > 0.1f)
        {
            float step = speed * Time.deltaTime;
            VRPlayer.transform.position = Vector3.MoveTowards(VRPlayer.transform.position, target, step);
            yield return null;
        }
        questions[CurrentPos - 1].SetActive(true);
    }
}
