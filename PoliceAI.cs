using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceAI : MonoBehaviour
{
    // Start is called before the first frame update
    [NonSerialized] public NavMeshAgent navMeshAgent;
    [NonSerialized] public GameObject Player;
    //public bool isWalking = true;
    //public bool isPunching = false;
    void Start()
    {
        Player = GameObject.Find("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) > 3)
        {
            navMeshAgent.SetDestination(Player.transform.position);
            navMeshAgent.isStopped = false;
        }
        else
        {
            transform.LookAt(Player.transform.position);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            navMeshAgent.isStopped = true;
        }
    }
}
