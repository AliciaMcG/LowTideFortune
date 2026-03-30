using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

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
    public Transform interactable; //e
    public Transform pullable; //f

    [Header("Movement")]
    public Vector3 wasdInput;
    public Vector3 velocity;
    public bool isSprinting;
    public bool isJumping;
    public int currRoom;

    //[Header("Head Bob")]   //FIX
    //public bool headBobIsActive;
    //public float bobStrength;
    //public float bobFreq;

    [Header("Player Object Interaction")]
    public Transform pickedObject; //FIX (move)
    public RaycastHit hit;
    public bool cast;

    [Header("Sounds")]
    public AudioSource walkingSound;
    public AudioSource pickupSound;
    public AudioSource placeSound;
    public AudioSource cauldronSound;
    public AudioSource entityNormalSound;
    public AudioSource entityChaseSound;

    [Header("Animator")]
    public Animator playerAnimator;

    public Transform entityPos; 
    public Transform cauldronPos; 
    public bool entityChasing;

    //game mode selected
    [Header("Gamemode Selection")]
    public static bool desktopMode;
    
    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
        //enable the different players and menu cameras for the chosen gamemode
        if (desktopMode == true)
        {
            gameplayBase.instance.desktopPlayer.SetActive(true);
            gameplayBase.instance.vrPlayer.SetActive(false);
            gameplayBase.instance.setDesktopMode();
        }
        else
        {
            gameplayBase.instance.desktopPlayer.SetActive(false);
            gameplayBase.instance.vrPlayer.SetActive(true);
            gameplayBase.instance.setVrMode();
        }
    }
    void Start()
    {
        velocity.z = 1;
        isSprinting = false;

        playerHealth = 3;
        pickedObject = null;
    }

    void Update()
    {
        castPlayerRay();
        //play certain sounds depending on the players distance from different objects
        checkEntityDistance(); //FIX make event?
        checkCauldronDistance();        
    }

    private void FixedUpdate()
    {
        //if the game's not paused
        if (sceneManager.gameIsPaused == false)
        {
            //if in desktop mode, move the player manually
            if (playerBase.desktopMode == true)
            {
                movePlayer();
            }
        }
    }

    private void LateUpdate()
    {
        //if in vr mode, move the player using xr controls
        if (playerBase.desktopMode == false)
        {
            Debug.Log(controller.name);
            Debug.Log(controller.transform.position);
        }

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
                playerAnimator.SetFloat("walkSpeed", 2f);
            }
            else
            {
                playerAnimator.SetFloat("walkSpeed", 1f);
            }

            // JUMPING
            if (velocity.y < 0)            {
                velocity.y = -1f;
            }
            if (isJumping)            {
                velocity.y = playerScriptable.jumpForce;
                
                //jump animation
                playerAnimator.SetTrigger("jump");
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

        // play the walking sound and animation
        if ((wasdInput.x != 0 || wasdInput.z != 0) && controller.isGrounded)        {
            if (!walkingSound.isPlaying)            {
                walkingSound.Play();
                playerAnimator.SetBool("walking", true);
            }
        }
        else        {
            walkingSound.Stop();
            playerAnimator.SetBool("walking", false);
        }
    }

    private void castPlayerRay()
    {
        //if the game's not paused
        if (sceneManager.gameIsPaused == false)
        {
            cast = Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 5f); //VAR
            Debug.Log(
                "Pos: " + playerCam.transform.position +
                " | Rot (Euler): " + playerCam.transform.eulerAngles +
                " | Forward: " + playerCam.transform.forward
            );
            if (cast)
            {
                Debug.Log("Ray hit: " + hit.collider.name);
                if (hit.collider.GetComponent<IInteractable>() != null)
                {
                    interactable = hit.collider.transform;
                    pullable = null;
                }
                else if (hit.collider.GetComponent<IPullable>() != null) 
                { 
                    pullable = hit.collider.transform;
                    interactable = null;
                }
                else
                {
                    interactable = null;
                    pullable = null;
                    //Debug.Log(" Didnt hit an interactable or pullable");
                }

                // CROSSHAIR RED OR BLACK + DEBUGS
                playerCam.crosshair.color = interactable != null || pullable != null ? playerCam.cursorScriptable.interactCross : playerCam.cursorScriptable.normalCross;
                if (pullable != null) { Debug.Log("hit pullable: " + hit.collider.name); }
                if (interactable != null) { Debug.Log("hit interactable: " + hit.collider.name); }
                //Debug.Log("Ray hit: " + hit.collider.name);
            }
            else
            {
                pullable = null;
                interactable = null;
                playerCam.crosshair.color = playerCam.cursorScriptable.normalCross;
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //if the game's not paused
        if (sceneManager.gameIsPaused == false)
        {
            if (context.started)        {

                if (interactable == null && pickedObject == null)
                {
                    Debug.Log("nothing interactable");
                }
                //if nothing to pickup or interact but still drop/place
                else if (interactable == null && pickedObject != null)
                {
                    pickedObject.GetComponent<pickupInteractable>().undoPickup(this); 

                    if (!placeSound.isPlaying)
                    {
                        placeSound.Play();
                        playerAnimator.SetTrigger("place");
                    }

                }
                else if (interactable != null)
                {
                    //pickup, if holding sum drop first
                    if (interactable.GetComponent<pickupInteractable>() != null)
                    {

                        if (pickedObject != null) { pickedObject.GetComponent<pickupInteractable>().undoPickup(this); }

                        if (!pickupSound.isPlaying)
                        {
                            pickupSound.Play();
                            playerAnimator.SetTrigger("pickup");
                        }

                        interactable.GetComponent<pickupInteractable>().interact(this);
                    }                
                    //Press button
                    else if (interactable.GetComponent<buttonBase>() != null)
                    {

                        interactable.GetComponent<buttonBase>().interact(this);
                    }
                    //Painting Dialogue Hint 
                    else if (interactable.GetComponent<paintingBase>() != null)
                    {

                        interactable.GetComponent<paintingBase>().interact(this);

                    }
                    //snapping objects (tarot cards, skulls)
                    else if (interactable.GetComponent<snapInteractable>() != null)
                    {

                        interactable.GetComponent<snapInteractable>().interact(this);
                        if (!placeSound.isPlaying)
                        {
                            placeSound.Play();
                            playerAnimator.SetTrigger("place");
                        }

                    }
                    else { Debug.Log("Whelp, check OnInteract"); }
                }
            }     
        }  
    }
    public void OnOpen(InputAction.CallbackContext context)
    {
        //if the game's not paused
        if (sceneManager.gameIsPaused == false)
        {
            if (context.started)         { 
                if (pullable != null)                {
                    pullable.GetComponent<IPullable>().pull(this);
                    playerAnimator.SetTrigger("pickup");
                }
                else { Debug.Log("Not an openable"); }

            }
        }
    }
    //vr interactions
    public void OnActivated(SelectEnterEventArgs args)
    {
        //if the game's not paused
        if (sceneManager.gameIsPaused == false)
        {
            var vrInteractable = args.interactableObject.transform;

            if (vrInteractable.GetComponent<IPullable>() != null)
            {
                vrInteractable.GetComponent<IPullable>().pull(this);
            }
            else if (vrInteractable.GetComponent<buttonBase>() != null)
            {
                vrInteractable.GetComponent<buttonBase>().interact(this);
            }
            //Painting Dialogue Hint 
            else if (vrInteractable.GetComponent<paintingBase>() != null)
            {
                vrInteractable.GetComponent<paintingBase>().interact(this);
            }
            else if (vrInteractable.GetComponent<bookInteract>() != null)
            {
                vrInteractable.GetComponent<bookInteract>().ToggleBookMode(true);
            }
            //snapping objects (tarot cards, skulls)
            else if (vrInteractable.GetComponent<snapInteractable>() != null)
            {

                vrInteractable.GetComponent<snapInteractable>().interact(this);
                if (!placeSound.isPlaying)
                {
                    placeSound.Play();
                    playerAnimator.SetTrigger("place");
                }

            }    
        }
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (sceneManager.gameIsPaused == false)
        {
            var vrInteractable = args.interactableObject.transform;

            if (vrInteractable.GetComponent<pickupInteractable>() != null)
            {
                if (!pickupSound.isPlaying)
                {
                    pickupSound.Play();
                    playerAnimator.SetTrigger("pickup");
                }

            }      
        }
    }
    public void OnSelectExited(SelectEnterEventArgs args)
    {
        if (sceneManager.gameIsPaused == false)
        {
            var vrInteractable = args.interactableObject.transform;

            if (vrInteractable.GetComponent<pickupInteractable>() != null)
            {
                if (!placeSound.isPlaying)
                {
                    placeSound.Play();
                    playerAnimator.SetTrigger("place");
                }

            }      
        }
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
