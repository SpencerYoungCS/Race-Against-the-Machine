using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public bool isAlive = true;
    public bool OnFloor = false;
    public bool takingDamage = false;
    public bool holdingObj = false;
    public float maxCarryWeight = 0.8f;
    public float throwStrength = 10f;

    void Start()
    {
        health = maxHealth;
    }

    //need a way to make sure health doesnt go over 100
    private void Update()
    {
        if (health > maxHealth)
            health = maxHealth;


        if (health <= 0)
        {
            isAlive = false;
            health = 0;
        }
        else
            isAlive = true;
    }

    public void Hurt(int damage)
    {
        if (!takingDamage)
            StartCoroutine(TakeDamage(damage));
    }

    void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.collider.name);
        GameObject obj = collision.gameObject;
        GameObject parent;
        try
        {
            parent = obj.transform.parent.gameObject;
            OnFloor = parent.name == "Floor";
        }
        catch { OnFloor = false;}

    }


    IEnumerator TakeDamage(int damage)
    {
        takingDamage = true;

        health = health <= damage ? 0 : health - damage;

        yield return new WaitForSeconds(.7f);
        takingDamage = false;

    }

}
