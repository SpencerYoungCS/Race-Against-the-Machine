using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontLookDown : MonoBehaviour 
{
    // Start is called before the first frame update
    public Camera playerCam;
    public GameObject fallingCam;
    public float angle = 42.5f;
    GameObject floor;
    public bool triggered = false;

    public void Start()
    {
        floor = GameObject.Find("Floor");
    }

    public void Update()
    {
        // Simple script to delete the floor if you look down
        if (playerCam.transform.eulerAngles.x > angle && playerCam.transform.eulerAngles.x < 100)
        {
            GameObject.Find("Player").GetComponent<PlayerCharacter>().health = 0;
            floor.gameObject.SetActive(false);
            playerCam.enabled = false;
            fallingCam.SetActive(true);
            triggered = true;
        }
    }
}
