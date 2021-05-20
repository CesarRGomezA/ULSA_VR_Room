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
        CurrentPos = 0;
        VRPlayer.transform.localPosition = new Vector3(0f, -0.17f, 0f);
        VRPlayer.XRRig.position = Positions[CurrentPos].position;
        VRPlayer.GetComponent<Rigidbody>().WakeUp();
    }

    public void MoveToNextStep()
    {
        VRPlayer.GetComponent<Rigidbody>().isKinematic = false;
        StopCoroutine("MoveToPoint");
        StartCoroutine(MoveToPoint(VRPlayer, Positions[++CurrentPos].position, moveSpeed));
    }

    public void MoveBack()
    {
        bridges.GetComponent<Collider>().enabled = false;
        bridges.SetActive(false);
    }

    public void ResetPlayer()
    {
        bridges.GetComponent<Collider>().enabled = false;
        bridges.SetActive(true);
        CurrentPos = 0;
        VRPlayer.transform.rotation = Quaternion.Euler(new Vector3(0f,-90f,0f));
        VRPlayer.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f); 
        VRPlayer.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f,0f,0f);
        VRPlayer.transform.localPosition = new Vector3(0f, -0.17f, 0f);
        VRPlayer.XRRig.position = Positions[CurrentPos].position;
        VRPlayer.GetComponent<Rigidbody>().isKinematic = true;

        MoveToNextStep();
        foreach(GameObject question in questions)
        {
            question.GetComponent<QuestionController>().FillQuestion();
        }
    }

    IEnumerator MoveToPoint(VRCamera VRPlayer, Vector3 target, float speed)
    {
        while (Vector3.Distance(VRPlayer.XRRig.position, target) > 0.1f)
        {
            float step = speed * Time.deltaTime;
            VRPlayer.XRRig.position = Vector3.MoveTowards(VRPlayer.XRRig.position, target, step);
            yield return null;
        }
        questions[CurrentPos - 1].SetActive(true);
    }
}
