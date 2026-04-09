using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class bookInteract : MonoBehaviour
{
    public GameObject bookUIPanel;
    private bool playerInRange;
    public InputActionReference openBook;

    public MonoBehaviour playerMovement;
    public camMover mouseLookScript;
    public GameObject crosshair;

    void Start()
    {
        bookUIPanel.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Q) || openBook.action.WasPressedThisFrame() )
        {
            bool isOpening = !bookUIPanel.activeSelf;
            ToggleBookMode(isOpening);
        }
    }

    public void ToggleBookMode(bool isReading)
    {
        bookUIPanel.SetActive(isReading);

        // 1. Freeze Movement AND Looking
        if (playerMovement != null) playerMovement.enabled = !isReading;
        if (mouseLookScript != null) mouseLookScript.enabled = !isReading;

        // 2. Manage the Mouse Cursor
        if (isReading)
        {
            mouseLookScript.transform.rotation = Quaternion.Euler(-90f, mouseLookScript.camRotation.y, 0);

            Cursor.lockState = CursorLockMode.Confined; // Let the mouse move freely
            Cursor.visible = true;                  // Show the system cursor
            if (crosshair != null) crosshair.SetActive(false); // Hide the crosshair
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock mouse to center
            Cursor.visible = false;                   // Hide system cursor
            if (crosshair != null) crosshair.SetActive(true); // Show crosshair
        }
    }

    /*
    void SetPlayerState(bool canMove)
    {
        // 1. Disable/Enable the scripts
        if (playerMovement != null) playerMovement.enabled = canMove;

        // 2. Handle the Mouse
        Cursor.lockState = canMove ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !canMove;

        // 3. THE HARD STOP: Kill existing momentum
        if (!canMove && playerMovement != null)
        {
            Rigidbody rb = playerMovement.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Something entered book trigger");
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player entered book range");
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player left book range");
            playerInRange = false;
        }
    }
}
