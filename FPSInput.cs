using UnityEngine;
using System.Collections;

// basic WASD-style movement control
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{

    [System.NonSerialized] public bool isCrouching = false;
    [System.NonSerialized] public bool isWalkingBackwards = false;
    public float JumpPower = 13f;
    private CapsuleCollider capsuleCollider;
    private PlayerCharacter player;
    GameObject conditions;
    private float speed = 3.0f;
    private float SpeedMultiplier;
    private float deltaX;
    private float deltaZ;
    public float gravity = 35f;
    public bool isGrounded;
    private float maxSpeed;

    // this will allow other scripts to alter the speed
    public float defaultSpeed;
    public float defaultSpeedMultiplier;
    //i need a way for the speed to stop changing since i want it to be more modular. so i should put a bool trigger

    void Start()
    {
        conditions = GameObject.Find("Conditions");
        player = GetComponent<PlayerCharacter>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        speed = defaultSpeed;
        maxSpeed = speed + 3f;
        SpeedMultiplier = defaultSpeedMultiplier;
    }


    void FixedUpdate()
    {
        //this looks at the parent's playerCharacter and checks its health which allows me to disable movement when they die
        if (player.isAlive)
        {

            // Inverse if camera is flipped
            deltaX = conditions.GetComponent<CameraFlip>().flipped ? -(Input.GetAxis("Horizontal")) : Input.GetAxis("Horizontal");

            deltaZ = Input.GetAxis("Vertical");

            Vector3 targetVelocity = new Vector3(deltaX, 0f, deltaZ);

            targetVelocity *= speed;
            targetVelocity = transform.TransformDirection(targetVelocity);
            Vector3 velocityChange = (targetVelocity - GetComponent<Rigidbody>().velocity);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                float distance = Vector3.Distance(transform.position, hit.point);
                //this checks if the player is airbourne
                //Debug.Log(distance);
                if (distance > 1.69f)
                {
                    gravity = 35f;
                    GetComponent<Collider>().material.staticFriction = 0.0f;
                    GetComponent<Collider>().material.dynamicFriction = 0.0f;
                    GetComponent<Collider>().material = GetComponent<Collider>().material;
                    isGrounded = false;
                }
                else
                {

                    if (hit.transform.name != "PostProcessing")
                    {
                        if (GetComponent<Rigidbody>().velocity.magnitude < .6)
                        {
                            gravity = 0;
                            GetComponent<Rigidbody>().velocity = Vector3.zero;
                        }
                        else
                            gravity = 35f;

                        GetComponent<Collider>().material.staticFriction = 0.6f;
                        GetComponent<Collider>().material.dynamicFriction = 0.6f;
                        GetComponent<Collider>().material = GetComponent<Collider>().material;
                        isGrounded = true;
                    }
                }
            }

            // Setting max vertical speed
            velocityChange.y = GetComponent<Rigidbody>().velocity.y > maxSpeed ? -1f : 0f;

            //Apply changes
            GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);


            // Movement Key Inputs
            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift))
                SpeedMultiplier = defaultSpeedMultiplier / 1.5f;
            else
                isWalkingBackwards = false;

            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S) && isGrounded)
                SpeedMultiplier = defaultSpeedMultiplier * 2f;

            if (Input.GetKey(KeyCode.Space) && isGrounded)
                GetComponent<Rigidbody>().AddForce(new Vector3(0, JumpPower, 0), ForceMode.Impulse);

            //make sure they dont cheat by pressing run and croutch at the same time
            if ((!Input.GetKey(KeyCode.LeftShift)) && (Input.GetKey(KeyCode.LeftControl)))
            {
                SpeedMultiplier = defaultSpeedMultiplier / 1.5f;
                isCrouching = true;
                capsuleCollider.height = 0.8f;
            }
            else
            {
                isCrouching = false;
                capsuleCollider.height = 2.1f;
            }

            //reset speed modified if player isnt using either crouch or run
            if ((!Input.GetKey(KeyCode.LeftShift)) && (!Input.GetKey(KeyCode.LeftControl)))
                SpeedMultiplier = defaultSpeedMultiplier;

            // after everything has been adjusted, alter the speed!
            speed = defaultSpeed * SpeedMultiplier;

        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            isCrouching = false;
            isWalkingBackwards = false;
        }

        // gravity is always applied
        GetComponent<Rigidbody>().AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));
    }
}