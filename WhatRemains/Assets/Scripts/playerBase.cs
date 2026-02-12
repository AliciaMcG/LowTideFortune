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

    [Header("Movement")]
    public float playerSpeed;
    public float jumpForce = 7f;
    public float gravity = -20f;

    public Vector3 wasdInput;
    public Vector3 velocity;
    public bool isSprinting;

    public Transform camOrient;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        velocity.z = 1;
        isSprinting = false;
    }

    void Update()
    {
        checkNmove();

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
                velocity.z = Mathf.Clamp(velocity.z, 1f, 1.7f);
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
            velocity.z = Mathf.Clamp(velocity.z, 1f, 1.2f);
        }


        velocity.y += gravity * Time.deltaTime;
        Vector3 finalMove = moveVector * playerSpeed * velocity.z + Vector3.up * velocity.y;

        controller.Move(finalMove * Time.deltaTime);
    }
}
