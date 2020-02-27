using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool isAlive = true;

    public int health = 100;


    void Start()
    {
        //health = 100;
        //isAlive = true;
        //isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        isAlive = health > 0 ? true : false;
    }

    public void Respawn()
    {
        health = 100;
        isAlive = true;
    }
}
