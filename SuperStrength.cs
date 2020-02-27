using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperStrength : MonoBehaviour
{
    PlayerCharacter player;
    private float defaultCarryWeight;
    public float maxCarryWeight = 30f;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        defaultCarryWeight = player.maxCarryWeight;
        player.maxCarryWeight = maxCarryWeight;
    }
    private void OnDisable()
    {
        player.maxCarryWeight = defaultCarryWeight;
    }
}
