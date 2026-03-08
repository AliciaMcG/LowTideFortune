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
    public Animator animator;

    public AudioSource doorOpenSound;
    public AudioSource doorCloseSound;


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
        if (doorIsOpen) 
        { 
            //play door open sound
            //doorOpenSound.Play();

            animator.SetTrigger("Open");
            //Debug.Log("door open"); 
        }
        else 
        {
            //play door close sound
            //doorCloseSound.Play();

            animator.SetTrigger("Close");
            //Debug.Log("door closed"); 
        }
    }
}

 