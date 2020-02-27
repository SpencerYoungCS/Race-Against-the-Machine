using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerCam : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        player.GetComponent<FPSInput>().gravity = 35f;
        transform.position = player.transform.position;
    }

    void Update()
    {
        transform.LookAt(player.transform.position);
    }
}
