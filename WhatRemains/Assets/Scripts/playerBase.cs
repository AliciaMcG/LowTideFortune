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
    [Header("GameObjects")]
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

    [Header("Interaction")]
    public float interactDist;
    public float attachedDist; //FIX (move)
    public Transform pickedObject; //FIX (move)
    public GameObject candleList; //FIX (make record current parent)
    public RaycastHit hit;
    public bool cast;

    [Header("Object Interaction")]
    public IInteractable interactableObj;

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

    //input system
    private InputSystem_Actions input;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
        pickedObject = null;
        input = new InputSystem_Actions();
    }
    void Start()
    {
        velocity.z = 1;
        isSprinting = false;

        entityChasing = false;
    }

    private void OnEnable()
    {
        input.Player.Enable();
        input.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        input.Player.Interact.performed -= OnInteract;
        input.Player.Disable();
    }

    void Update()
    {
        checkNmove();

        //play certain sounds depending on the players distance from different objects
        checkEntityDistance();
        checkCauldronDistance();

        // pickup / put down
        cast = Physics.Raycast(playerCam.position, playerCam.forward, out hit, attachedDist);
        interactableObj = null;

        if (cast)
        {
            if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable)) {
                interactableObj = interactable;
            }
            Debug.Log("Ray hit: " + hit.collider.name);
        }
        if (interactableObj == null) { Debug.Log("no interactable"); }
        

        if (Input.GetKeyDown(KeyCode.F) && tarotCards.pointingAtTargetPos == false) {
            if (pickedObject != null) {     // Put down

                //play placing sound
                if (!placeSound.isPlaying)
                {
                    placeSound.Play();
                    Debug.Log("placing sound playing");

                }

                pickedObject.SetParent(candleList.transform); //FIX (make return)
                if (pickedObject.GetComponent<Rigidbody>() != null) { pickedObject.GetComponent<Rigidbody>().isKinematic = false; }
                if (pickedObject.GetComponent<Collider>() != null) { pickedObject.GetComponent<Collider>().enabled = true; }

                pickedObject = null;
            }
            else
            {             // pickup
                if (cast)
                {
                    if (hit.transform.CompareTag("pickupAble"))
                    {
                        //play picking up sound
                        if (!pickupSound.isPlaying)
                        {
                            pickupSound.Play();
                        }

                        pickedObject = hit.transform;
                        pickedObject.SetParent(controller.transform);

                        if (pickedObject.GetComponent<Rigidbody>() != null) { pickedObject.GetComponent<Rigidbody>().isKinematic = true; }
                        if (pickedObject.GetComponent<Collider>() != null) { pickedObject.GetComponent<Collider>().enabled = false; }
                    }
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

        // play the walking sound
        if ((wasdInput.x != 0 || wasdInput.z != 0) && controller.isGrounded)
        {
            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
        }
        else
        {
            walkingSound.Stop();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (interactableObj != null) {
                Debug.Log("Interacting with door");
                interactableObj.interact(this);
            }            
        }
    }

    //plays a boiling sound when the player is close enough to the cauldron
    public void checkCauldronDistance()
    {
        //get the distance between the player and the cauldron
        float distance = Vector3.Distance(cauldronPos.position, playerCam.position);

        //if the distance is within 1, play the boiling sound
        if (distance <= 1.0)
        {
            Debug.Log("next to cauldron");
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
        float distance = Vector3.Distance(entityPos.position, playerCam.position);

        //if the distance is within 1, play the entity sound
        if (distance <= 5.0)
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
        }
    }

}
