using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoliceAnimController : MonoBehaviour
{
    PoliceAI policeAI;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        policeAI = GetComponent<PoliceAI>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", !policeAI.navMeshAgent.isStopped);
        animator.SetBool("isPunching", policeAI.navMeshAgent.isStopped);
    }
}
