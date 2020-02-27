using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour, Interactable
{
    WaterZooController roomController;
    public void Interact()
    {
        roomController.catsSaved++;
        if(roomController.catsSaved == 10)
        {
            roomController.Success();
        }
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        roomController = GameObject.Find("RoomController").GetComponent<WaterZooController>();
    }
}
