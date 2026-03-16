using UnityEngine;
using UnityEngine.InputSystem;

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
    public int playerHealth;
    
    [Header("Objects")]
    public playerScriptable playerScriptable;
    public CharacterController controller;
    public camMover playerCam;
    public Transform camOrient;
    public GameObject playerUIScreen; //pause n gameover
    public Transform pickable;
    public Transform pullable;

    [Header("Movement")]
    public Vector3 wasdInput;
    public Vector3 velocity;
    public bool isSprinting;
    public bool isJumping;

    //[Header("Head Bob")]   //FIX
    //public bool headBobIsActive;
    //public float bobStrength;
    //public float bobFreq;

    [Header("Player Object Interaction")]
    public Transform pickedObject; //FIX (move)
    public RaycastHit hit;
    public bool cast;
    public GameObject candleList; //FIX (make record current parent)

    [Header("Sounds")]
    public AudioSource walkingSound;
    public AudioSource pickupSound;
    public AudioSource placeSound;
    public AudioSource cauldronSound;
    public AudioSource entityNormalSound;
    public AudioSource entityChaseSound;

    public Transform entityPos; 
    public Transform cauldronPos; 
    public bool entityChasing;


    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
    }
    void Start()
    {
        velocity.z = 1;
        isSprinting = false;
        playerHealth = 3;

        entityChasing = false;
        pickedObject = null;
    }

    void Update()
    {
        castPlayerRay();


        //play certain sounds depending on the players distance from different objects
        checkEntityDistance(); //FIX make event?
        checkCauldronDistance();        

        if (Input.GetKeyDown(KeyCode.E) && tarotCards.pointingAtTargetPos == false) {
  
                if (cast) //FIX later eep now
                {

                    if (hit.transform.CompareTag("chair"))
                    {
                        //play picking up sound
                        if (!pickupSound.isPlaying)
                        {
                            pickupSound.Play();
                        }

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

    private void FixedUpdate()
    {
        movePlayer();
    }

    private void LateUpdate()
    {
        if (pickedObject != null)
        {
            //hold object position
            pickedObject.transform.position = camOrient.position + camOrient.up * 4f + playerCam.transform.forward * playerScriptable.attachedDist;
        }
    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    public void OnMove(InputAction.CallbackContext context)
    {
        wasdInput.x = context.ReadValue<Vector2>().x;
        wasdInput.z = context.ReadValue<Vector2>().y;
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed) { isSprinting = true; }
        if (context.canceled) { isSprinting = false; }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) { isJumping = true; }
        if (context.canceled) { isJumping = false; }
    }

    private void movePlayer()
    {
        Vector3 moveVector = camOrient.forward * wasdInput.z + camOrient.right * wasdInput.x;
        moveVector = Vector3.ClampMagnitude(moveVector, 1f);

        // SPRINTING

        if (controller.isGrounded)        {
            if (isSprinting)            {
                velocity.z += 0.08f;
                velocity.z = Mathf.Clamp(velocity.z, 1f, 1.7f);
            }

            // JUMPING
            if (velocity.y < 0)            {
                velocity.y = -1f;
            }
            if (isJumping)            {
                velocity.y = playerScriptable.jumpForce;
            }
        }

        //DECELERATION / Velocity reset / Only forward sprint
        if (!isSprinting && (velocity.z > 1))        {
            velocity.z -= 0.01f;
        }
        if (velocity.z < 1 || wasdInput.z < 0.5) { velocity.z = 1; }
        // NO JUMP SPRINT FLYING
        if (!controller.isGrounded)        {
            velocity.z = Mathf.Clamp(velocity.z, 1f, 1.18f);
        }


        velocity.y += playerScriptable.gravity * Time.deltaTime;
        Vector3 finalMove = moveVector * playerScriptable.playerSpeed * velocity.z + Vector3.up * velocity.y;

        controller.Move(finalMove * Time.deltaTime);

        // play the walking sound
        if ((wasdInput.x != 0 || wasdInput.z != 0) && controller.isGrounded)        {
            if (!walkingSound.isPlaying)            {
                walkingSound.Play();
            }
        }
        else        {
            walkingSound.Stop();
        }
    }

    private void castPlayerRay()
    {
        cast = Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 5f); //VAR
        if (cast)
        {
            if (hit.collider.GetComponent<pickupAble>() != null)
            {
                pickable = hit.collider.transform;
                pullable = null;
            }
            else if (hit.collider.GetComponent<IPullable>() != null) 
            { 
                pullable = hit.collider.transform;
                pickable = null;
            }
            else
            {
                pullable = null;
                pickable = null;
                //Debug.Log(" Didnt hit an interactable");
            }

            playerCam.crosshair.color = pickable != null || pullable != null ? playerCam.cursorScriptable.interactCross : playerCam.cursorScriptable.normalCross;
            if (pullable != null) { Debug.Log("hit pullable: " + hit.collider.name); }
            if (pickable != null) { Debug.Log("hit pickable: " + hit.collider.name); }
            //Debug.Log("Ray hit: " + hit.collider.name);
        }
        else
        {
            pullable = null;
            pickable = null;
            playerCam.crosshair.color = playerCam.cursorScriptable.normalCross;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)        {

            if (pickable != null)
            {
                if (pickedObject != null) { undoPickup(); }

                if (!pickupSound.isPlaying)
                {
                    pickupSound.Play();
                }

                pickable.GetComponent<pickupAble>().pickup(this);
                if (pickedObject != null) { 
                    pickedObject.GetComponent<Rigidbody>().isKinematic = true;
                    pickedObject.GetComponent<Collider>().enabled = false;
                    //Debug.Log("Picked up: " + pickedObject.name);
                }
            }
            else if (pickedObject != null)
            {
                undoPickup();
                //Debug.Log("Dropped" + pickedObject.name);
            }
            else if (pickable == null) { Debug.Log("nothing pickable"); }
        }
       
    }
    public void OnOpen(InputAction.CallbackContext context)
    {
        if (context.started)         { 
            if (pullable != null)                {
                pullable.GetComponent<IPullable>().pull(this);
            }
            else { Debug.Log("Not an openable"); }

        }
    }

    private void undoPickup()
    {
        if (!placeSound.isPlaying)        {
            placeSound.Play();
        }

        pickedObject.GetComponent<Rigidbody>().isKinematic = false;
        pickedObject.GetComponent<Collider>().enabled = true;
        pickedObject = null;
    }

    //plays a boiling sound when the player is close enough to the cauldron
    public void checkCauldronDistance()
    {
        //get the distance between the player and the cauldron
        float distance = Vector3.Distance(cauldronPos.position, playerCam.transform.position);

        //if the distance is within 1, play the boiling sound
        if (distance <= 10.0)
        {
            if (!cauldronSound.isPlaying)
            {
                cauldronSound.Play();
            }
        }
        else
        {
            cauldronSound.Stop();
        }
    }
    //plays entity sounds when the player is close enough to the entity
    public void checkEntityDistance()
    {
        //get the distance between the player and the entity
        float distance = Vector3.Distance(entityPos.position, playerCam.transform.position);

        //if the distance is within 1, play the entity sound
        if (distance <= 15.0)
        {
            //if its chasing, play the footsteps
            if (!entityNormalSound.isPlaying)
            {
                entityNormalSound.Play();
            }
        
            if (entityChasing)
            {
                if (!entityChaseSound.isPlaying)
                {
                    entityChaseSound.Play();
                }
            }
            else
            {
                entityChaseSound.Stop();
            }
        }
        else
        {
            entityNormalSound.Stop();
            entityChaseSound.Stop();
        }
    }

}
