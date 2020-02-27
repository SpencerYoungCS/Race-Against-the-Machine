using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicAfter : MonoBehaviour
{
    // Start is called before the first frame update
    float speed;
    bool delaying;
    bool stopChecking;
    GameObject conditions;

    void Start()
    {
        conditions = GameObject.Find("Conditions");
        stopChecking = false;
    }

    private void Update()
    {
        speed = GetComponent<Rigidbody>().velocity.magnitude;
        if (!delaying && !stopChecking)
            StartCoroutine(DelayedKinematic(2f));

        if (conditions.GetComponent<DontLookDown>().triggered)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
        }

    }

    IEnumerator DelayedKinematic(float time)
    {
        delaying = true;
        yield return new WaitForSeconds(time);

        if (speed < 0.2)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
            stopChecking = true;

        }
        delaying = false;

    }
}
