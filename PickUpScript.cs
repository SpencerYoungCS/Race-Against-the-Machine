using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour, Interactable
{
    GameObject guide;
    PlayerCharacter player;

    bool isActive = false;
    public void Interact()
    {
        isActive = !isActive;
        player.holdingObj = isActive;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        guide = GameObject.Find("Guide");
    }

    void Update()
    {
        ChangePosition(isActive);
    }

    void ChangePosition(bool isActive)
    {
        if (isActive)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
            transform.position = guide.GetComponent<Transform>().position;
            transform.rotation = guide.GetComponent<Transform>().rotation;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
