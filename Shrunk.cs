using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrunk : MonoBehaviour
{
    PlayerCharacter player;
    public float PercentShrink = 75;
    void Start()
    {
        
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        player.transform.localScale -= new Vector3(PercentShrink/100,PercentShrink/100 ,PercentShrink/100);
    }

    private void OnDisable()
    {
        player.transform.localScale = new Vector3(1, 1, 1);
    }

}
