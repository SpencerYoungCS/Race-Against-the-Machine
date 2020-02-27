using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraFlip : MonoBehaviour
{
    // Start is called before the first frame update
    public bool flipped = false;
    public GameObject textMesh;
    float timeLeft = 10f;
    float flipTime;
    int minutes;
    int seconds;
    void Start()
    {
        textMesh.GetComponent<TextMeshProUGUI>().enabled = true;
        textMesh.GetComponent<TextMeshProUGUI>().text = "SCREEN FLIPS IN: " + timeLeft;
    }

    // Update is called once per frame
    void Update()
    {

        textMesh.GetComponent<TextMeshProUGUI>().text = "SCREEN FLIPS IN: " + seconds;
        if (!flipped)
        {
            // cleaner representation of time by user jashan
            minutes = Mathf.FloorToInt(timeLeft / 60F);
            seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
            //string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            //-----------------------
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                flipTime = Random.Range(3, 7);
                StartCoroutine(flip(flipTime));
            }
        }
        // wait for flipping to be finished
        else
        {
            flipTime -= Time.deltaTime;
            minutes = Mathf.FloorToInt(flipTime / 60F);
            seconds = Mathf.FloorToInt(flipTime - minutes * 60);
        }

    }

    IEnumerator flip(float flipTime)
    {
        flipped = true;
        yield return new WaitForSeconds(flipTime);
        flipped = false;
        timeLeft = Random.Range(5, 10);
    }
}
