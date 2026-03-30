using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class camMover : MonoBehaviour
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    [Header("Camera Attributes")]
    public Transform camOrient;
    public Vector2 camRotation;
    public Vector2 camSpeed;

    [Header("Objects")]
    public crosshairScriptable cursorScriptable;
    public Vector2 cursorMoveInp;
    public Image crosshair;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crosshair.sprite = cursorScriptable.crossSpr;
    }

    void Update()
    {
        
        if(playerBase.desktopMode == true)
        {
            moveCam();
        }
    }

    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    public void OnLook(InputAction.CallbackContext context)
    {
        if (sceneManager.gameIsPaused == false)
        {
            cursorMoveInp = context.ReadValue<Vector2>();
        }
    }
    public void moveCam()
    {
        float sensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        
        float lookX = cursorMoveInp.x * sensitivity;
        float lookY = cursorMoveInp.y * sensitivity;
        
        camRotation.y += cursorMoveInp.x * camSpeed.x * Time.fixedDeltaTime;
        camRotation.x -= cursorMoveInp.y * camSpeed.y * Time.fixedDeltaTime;
        camRotation.x = Mathf.Clamp(camRotation.x, -90f, 90f);

        transform.rotation = Quaternion.Euler(camRotation.x, camRotation.y, 0);
        camOrient.rotation = Quaternion.Euler(0, camRotation.y, 0);
    }
}
