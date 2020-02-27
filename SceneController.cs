using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Rigidbody[] allRigidBodies;
    public GameObject ConditionsObj;
    public int NumOfConditions;
    public bool RandomEnabled = true;
    public int HelpingHand = 0;

    List<MonoBehaviour> conditionsList = new List<MonoBehaviour>();

    void Start()
    {
        allRigidBodies = Object.FindObjectsOfType<Rigidbody>();
        foreach(Rigidbody item in allRigidBodies)
        {
            GameObject tempObj = item.transform.gameObject;
            try
            {
                Vector3 objectSize = Vector3.Scale(tempObj.transform.localScale, tempObj.GetComponent<MeshRenderer>().bounds.size);
                item.mass = Mathf.Abs(objectSize.x * objectSize.y * objectSize.z);
            }
            catch { }
        }

    }

    void OnEnable()
    {

        // Get all of the scripts (conditions) and put them in a list
        foreach(MonoBehaviour item in ConditionsObj.GetComponents<MonoBehaviour>())
            conditionsList.Add(item);

        // Enable a or multiple conditions
        if (RandomEnabled)
        {
            for (int i = 0; i < NumOfConditions; i++)
            {
                int rand = Random.Range(0, conditionsList.Count);
                conditionsList[rand].enabled = true;
            }
        }
    }
}
