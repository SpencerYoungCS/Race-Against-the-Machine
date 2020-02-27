using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.DynamicGI;

public class RayShooter : MonoBehaviour
{
    private PlayerCharacter player;
    private Camera _camera;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 point;
    private GameObject Floor;
    GameObject itemHeld;
    public GameObject guide;
    public float reach = 5.5f;
    private bool throwing;

    void Start()
    {
        Floor = GameObject.Find("Floor");
        throwing = false;
        _camera = GetComponent<Camera>();
        player = GetComponentInParent<PlayerCharacter>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (player.isAlive)
        {
            point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            ray = _camera.ScreenPointToRay(point);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                //Debug.Log(hitObject.name);
                //Debug.Log(Vector3.Distance(transform.position, hitObject.transform.position));
                if (Vector3.Distance(transform.position, hitObject.transform.position) < 5.5)
                {
                    //here i will check if the Interactable interface exists. and if it does, call it
                    if (Input.GetMouseButton(0))
                    {
                        //Debug.Log(hitObject.name);
                        Interactable interact = hitObject.GetComponent<Interactable>();
                        if (player.holdingObj && !throwing)
                            PickUpObj(itemHeld);

                        else if (!player.holdingObj)
                        {
                            if (interact != null)
                                interact.Interact();
                        }
                        else
                        {
                            try
                            {
                                if (hitObject.GetComponent<Rigidbody>().mass < player.maxCarryWeight)
                                {
                                    if (!player.holdingObj && !throwing)
                                    {
                                        itemHeld = hitObject.transform.gameObject;
                                        PickUpObj(itemHeld);

                                    }
                                    else if (Input.GetMouseButton(1) && !throwing)
                                    {
                                        DropObj(itemHeld);
                                        StartCoroutine(ThrowObj(itemHeld));
                                    }
                                }
                            }
                            catch
                            {
                                // Object is uninteractable
                            }
                        }
                    }
                    else
                    {
                        if (player.holdingObj)
                            DropObj(itemHeld);

                    }

                }
            }
        }
        //player died!
        else
        {
            if (player.holdingObj)
                DropObj(itemHeld);
        }
    }

    IEnumerator ThrowObj(GameObject hitObject)
    {
        throwing = true;
        hitObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * hitObject.GetComponent<Rigidbody>().mass * player.throwStrength, ForceMode.Impulse);
        yield return new WaitForSeconds(.8f);
        throwing = false;
    }

    private void PickUpObj(GameObject hitObject)
    {
        player.holdingObj = true;
        hitObject.GetComponent<Rigidbody>().isKinematic = true;
        hitObject.GetComponent<Rigidbody>().useGravity = false;
        // ignore collision with floor and player for the picked up object
        Physics.IgnoreCollision(hitObject.GetComponent<Collider>(), Floor.GetComponentInChildren<Collider>(), true);
        Physics.IgnoreCollision(hitObject.GetComponent<Collider>(), player.GetComponent<Collider>(), true);

        //hitObject.GetComponent<Rigidbody>().MovePosition(guide.transform.position);
        //hitObject.GetComponent<Rigidbody>().MoveRotation(guide.transform.rotation);

        hitObject.transform.position = guide.GetComponent<Transform>().position;
        hitObject.transform.rotation = guide.GetComponent<Transform>().rotation;
    }
    private void DropObj(GameObject hitObject)
    {
        player.holdingObj = false;
        Physics.IgnoreCollision(hitObject.GetComponent<Collider>(), Floor.GetComponentInChildren<Collider>(), false);
        Physics.IgnoreCollision(hitObject.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        hitObject.GetComponent<Rigidbody>().isKinematic = false;
        hitObject.GetComponent<Rigidbody>().useGravity = true;
    }
    IEnumerator ShowMessage(string message)
    {
        //showingMessage = true;

        //        alertedText.GetComponent<Text>().text = message;
        //        for (float i = 0; i < 1; i += 0.02f)
        //        {
        //            alertedText.GetComponent<CanvasGroup>().alpha = i;
        //            yield return new WaitForEndOfFrame();
        //        }
        //        yield return new WaitForSeconds(5.0f);
        //        for (float i = 1; i > 0; i -= 0.02f)
        //        {
        //            alertedText.GetComponent<CanvasGroup>().alpha = i;
        //            yield return new WaitForEndOfFrame();
        //        }
        yield return new WaitForSeconds(2);

        //showingMessage = false;

    }
}