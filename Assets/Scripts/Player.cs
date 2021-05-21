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
    
    public void StartPlayer()
    {
        CurrentPos = 0;
        VRPlayer.transform.localPosition = new Vector3(0f, -0.17f, 0f);
        VRPlayer.XRRig.position = Positions[CurrentPos].position;
        //VRPlayer.GetComponent<Rigidbody>().WakeUp();
    }

    public void MoveToNextStep()
    {
        //VRPlayer.GetComponent<Rigidbody>().isKinematic = false;
        StopCoroutine("MoveToPoint");
        StartCoroutine(MoveToPoint(VRPlayer, Positions[++CurrentPos].position, moveSpeed, CurrentPos == Positions.Length - 1));
    }

    public void MoveBack()
    {
        //bridges.GetComponent<Collider>().enabled = false;
        bridges.SetActive(false);
        StartCoroutine(Fall(VRPlayer, GameManager.instance.floorPosition.position, 5.3f));
    }

    public void ResetPlayer()
    {
        //bridges.GetComponent<Collider>().enabled = false;
        bridges.SetActive(true);
        CurrentPos = 0;
        VRPlayer.transform.rotation = Quaternion.Euler(new Vector3(0f,-90f,0f));
        //VRPlayer.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f); 
        //VRPlayer.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f,0f,0f);
        VRPlayer.transform.localPosition = new Vector3(0f, -0.17f, 0f);
        VRPlayer.XRRig.position = Positions[CurrentPos].position;
        //VRPlayer.GetComponent<Rigidbody>().isKinematic = true;

        MoveToNextStep();
        foreach(GameObject question in questions)
        {
            question.GetComponent<QuestionController>().FillQuestion();
        }
    }

    IEnumerator MoveToPoint(VRCamera VRPlayer, Vector3 target, float speed, bool lastPosition)
    {
        while (Vector3.Distance(VRPlayer.XRRig.position, target) > 0.1f)
        {
            float step = speed * Time.deltaTime;
            VRPlayer.XRRig.position = Vector3.MoveTowards(VRPlayer.XRRig.position, target, step);
            yield return null;
        }
        if(!lastPosition)
        { 
            questions[CurrentPos - 1].SetActive(true);
        }
    }

    IEnumerator Fall(VRCamera VRPlayer, Vector3 floorPosition, float gravity)
    {
        float speed = 0;
        while (VRPlayer.XRRig.position.y >= floorPosition.y)
        {
            speed += gravity * Time.deltaTime;
            float step = speed * Time.deltaTime;
            VRPlayer.XRRig.position = new Vector3(VRPlayer.XRRig.position.x, VRPlayer.XRRig.position.y - step, VRPlayer.XRRig.position.z);
            yield return null;
        }
        ResetPlayer();
    }
}
