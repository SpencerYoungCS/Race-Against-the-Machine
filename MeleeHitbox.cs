using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    //this makes enemy's melee attacks hurt me
    NPC enemy;
    public int damage = 20;
    public AudioSource[] audioSources;

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
        enemy = GetComponentInParent<NPC>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (enemy.isAlive)
        {
            if (other.name == "Player")
            {
                Debug.Log(audioSources.Length);
                int i = Random.Range(0, audioSources.Length);
                PlayerCharacter player = other.GetComponent<PlayerCharacter>();
                player.Hurt(damage);
                audioSources[i].PlayOneShot(audioSources[i].clip);
            }
        }
    }
}
