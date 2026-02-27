using UnityEngine;

/// <summary>
/// Holds code for:
/// 
///   Player movement
///   Player vars
///   
///   ad is x
///   ws is y
///   
///   using playercontroller since game is not particularly physics based
///   
/// </summary>
/// 


public class playerBase : MonoBehaviour
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    public CharacterController controller;
    public Transform camOrient;
    public Transform playerCam;

    [Header("Movement")]
    public float playerSpeed;
    public float jumpForce = 7f;
    public float gravity = -20f;

    public Vector3 wasdInput;
    public Vector3 velocity;
    public bool isSprinting;


    //[Header("Head Bob")]   //FIX
    //public bool headBobIsActive;
    //public float bobStrength;
    //public float bobFreq;

    [Header("Pickup")]
    public float pickupDist;
    public float attachedDist;
    public Transform pickedObject;
    public GameObject candleList;
    public RaycastHit hit;
    public bool cast;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
        pickedObject = null;
    }
    void Start()
    {
        velocity.z = 1;
        isSprinting = false;
    }

    void Update()
    {
        checkNmove();

        // pickup
        cast = Physics.Raycast(playerCam.position, playerCam.forward, out hit, pickupDist);

        if (Input.GetKeyDown(KeyCode.F) && tarotCards.pointingAtTargetPos == false) {
            if (pickedObject != null) {
                pickedObject.SetParent(candleList.transform);

                if (pickedObject.GetComponent<Rigidbody>() != null) {
                    pickedObject.GetComponent<Rigidbody>().isKinematic = false;
                }

                if (pickedObject.GetComponent<Collider>() != null) {
                    pickedObject.GetComponent<Collider>().enabled = true;
                }

                pickedObject = null;
            }
            else { 
                if (cast) {
                    if (hit.transform.CompareTag("pickupAble")) { 
                        pickedObject = hit.transform;
                        pickedObject.SetParent(controller.transform);

                        if (pickedObject.GetComponent<Rigidbody>() != null)
                        {
                            pickedObject.GetComponent<Rigidbody>().isKinematic = true;
                        }

                        if (pickedObject.GetComponent<Collider>() != null)
                        {
                            pickedObject.GetComponent<Collider>().enabled = false;
                        }
                    }
                    if (hit.transform.CompareTag("chair"))
                    {
                        //get the chair object
                        Transform chair;
                        chair = hit.transform;
                        //Debug.Log(chair);

                        //move the chair back 2 spaces
                        chair.Translate(Vector3.right * 2.0f, Space.Self);

                        //disable the collider
                        chair.GetComponent<BoxCollider>().enabled = false;

                        //unparent the tarot card to pick up
                        Transform tarotCard = chair.Find("sagittariusTarotCard");
                        tarotCard.SetParent(null);
                    }
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (pickedObject != null) {
            pickedObject.position = camOrient.position + camOrient.forward * attachedDist;
        }
    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    private void checkNmove()
    {
        wasdInput.x = Input.GetAxisRaw("Horizontal");
        wasdInput.z = Input.GetAxisRaw("Vertical");

        Vector3 moveVector = camOrient.forward * wasdInput.z + camOrient.right * wasdInput.x;
        moveVector = Vector3.ClampMagnitude(moveVector, 1f);

        // SPRINTING
        if (Input.GetKey(KeyCode.LeftShift)) { isSprinting = true; }
        else {  isSprinting = false; }

        if (controller.isGrounded) {
            if (isSprinting) {
                velocity.z += 0.08f;
                velocity.z = Mathf.Clamp(velocity.z, 1f, 1.5f);
            }

            // JUMPING 
            if (velocity.y < 0) {
                velocity.y = -1f;
            }
            if (Input.GetKey(KeyCode.Space)) { 
                velocity.y = jumpForce;
            }
        }

        //DECELERATION / Velocity reset / Only forward sprint
        if (!isSprinting && (velocity.z > 1)) {
            velocity.z -= 0.1f;
        }
        if (velocity.z < 1 || wasdInput.z < 0.5) { velocity.z = 1; }
        // NO JUMP SPRINT FLYING
        if (!controller.isGrounded) {
            velocity.z = Mathf.Clamp(velocity.z, 1f, 1.18f);
        }


        velocity.y += gravity * Time.deltaTime;
        Vector3 finalMove = moveVector * playerSpeed * velocity.z + Vector3.up * velocity.y;

        controller.Move(finalMove * Time.deltaTime);
    }
}
