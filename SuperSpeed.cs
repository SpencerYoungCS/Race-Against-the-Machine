using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Player;
    float Multiplier = 3f;
    private float defaultMultiplier;

    void Start()
    {
        Player = GameObject.Find("Player");
        defaultMultiplier = Player.GetComponent<FPSInput>().defaultSpeedMultiplier;
        Player.GetComponent<FPSInput>().defaultSpeedMultiplier = Multiplier; 
    }

    private void OnDisable()
    {
        Player.GetComponent<FPSInput>().defaultSpeedMultiplier = defaultMultiplier; 
    }
}
