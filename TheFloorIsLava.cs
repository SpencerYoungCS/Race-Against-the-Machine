using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TheFloorIsLava : MonoBehaviour 
{
    // Start is called before the first frame update
    PlayerCharacter player;
    GameObject floor;
    public Shader lava;
    public List<GameObject> rocks;
    private Quaternion randomRotation;

    public void Start()
    {
        floor = GameObject.Find("Floor");
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();

        Vector3 boxSize = floor.GetComponentInChildren<Collider>().bounds.size;

        foreach(Renderer rend in floor.GetComponentsInChildren<Renderer>())
        {
            rend.material.shader = lava;
        }


        for (float i = -boxSize.x/2 + 1; i < boxSize.x / 2; i += boxSize.x / 8)
        {
            for (float j = -boxSize.z/2 + 1; j < boxSize.z / 2; j += boxSize.z / 8)
            {
                //defines a random rotation
                randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                //Instantiates the rock
                GameObject newRock = Instantiate(rocks[Random.Range(0, rocks.Count - 1)], new Vector3(i, 1, j), randomRotation);
                //Scale the rock accordingly to the size of the map
                newRock.transform.localScale = new Vector3(newRock.transform.localScale.x * (boxSize.x/100) , newRock.transform.localScale.y *(boxSize.x/100), newRock.transform.localScale.z * (boxSize.x/100));
                //Random drag so if they fall together, it looks more natural
                newRock.GetComponent<Rigidbody>().drag = Random.Range(0f, 3f);
            }
        }
    }

    public void Update()
    {
        if (player.OnFloor)
            player.Hurt(10);
    }

}
