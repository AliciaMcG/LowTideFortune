using UnityEngine;

/// <summary>
/// Holds code for:
/// 
///   Player movement
///   Player vars
///   
///   ad is y
///   ws is x
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

    public Vector2 wasdInput;
    public Vector3 velocity;
    public Transform camOrient;

    public float playerHeight;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
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
        wasdInput.y = Input.GetAxisRaw("Vertical");

        Vector3 moveVector = camOrient.forward * wasdInput.y + camOrient.right * wasdInput.x;
        moveVector = Vector3.ClampMagnitude(moveVector, 1f);

        // JUMPING 
        if (controller.isGrounded)
        {
            if (velocity.y < 0) { 
                velocity.y = -1f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
                velocity.y = jumpForce;
        }

        velocity.y += gravity * Time.deltaTime;
        Vector3 finalMove = moveVector * playerSpeed + Vector3.up * velocity.y;
        controller.Move(finalMove * Time.deltaTime);
    }


}
