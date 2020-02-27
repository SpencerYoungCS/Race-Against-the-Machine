using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Dont forget to add AudioSource component to player
public class Sausage : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(sausage());
    }

    IEnumerator sausage()
    {
        int randomSeconds = Random.Range(5, 7);
        yield return new WaitForSeconds(randomSeconds);
        audioSource.PlayOneShot(audioSource.clip);
        StartCoroutine(sausage());
    }
}
