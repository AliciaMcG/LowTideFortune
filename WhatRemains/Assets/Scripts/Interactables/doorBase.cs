using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Holds code for:
/// 
///   ///
///   
/// </summary>

public class doorBase : MonoBehaviour, IInteractable
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    public bool doorIsOpen;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
    }

    void Start()
    {
        doorIsOpen = false;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {

    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    private void OnTriggerEnter(Collider other)    {
        if (other.TryGetComponent(out playerBase player)) 
        { 
            player.interactableObj = this; 
            //Debug.Log("player colliding with door");
        }
    }
    private void OnTriggerExit(Collider other)    {
        if (other.TryGetComponent(out playerBase player)) { player.interactableObj = null; }
    }
    public void interact(playerBase player)
    {
        doorIsOpen = !doorIsOpen;
        if (doorIsOpen) { Debug.Log("door open"); }
        else { Debug.Log("door closed"); }
    }
}

 