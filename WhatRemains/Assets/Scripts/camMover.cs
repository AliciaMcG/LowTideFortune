using UnityEngine;

public class camMover : MonoBehaviour
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    public Camera playerCam;
    public Vector2 camSpeed;
    public Transform camOrient;
    public Vector2 camRotation;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        checkCursorPlace();
        moveCam();
    }

    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    private void checkCursorPlace()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * camSpeed.x * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * camSpeed.y * Time.deltaTime;

        camRotation.y += mouseX;
        camRotation.x -= mouseY;
        camRotation.x = Mathf.Clamp(camRotation.x, -90f, 90f);
    }
    private void moveCam()
    {
        transform.rotation = Quaternion.Euler(camRotation.x, camRotation.y, 0);
        camOrient.rotation = Quaternion.Euler(0, camRotation.y, 0);
    }
}
