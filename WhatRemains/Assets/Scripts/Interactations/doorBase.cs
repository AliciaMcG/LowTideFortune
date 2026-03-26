using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Holds code for:
/// 
///   door opening and closing
///   
///   also room locking teehee :)
///   
///   door type to help lock
///     0 - always open
///     1 - locked until entity is spawned
///     2 - locked until dinner??
///     3 - locked until final chase
///   
/// </summary>

public class doorBase : MonoBehaviour, IPullable
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    public int doorType;
    public bool isAccessible;
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

        if (doorType != 0)
        {
            isAccessible = false;
        }
        else { isAccessible = true; }
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {

    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    //private void OnTriggerEnter(Collider other)    {
    //    if (other.TryGetComponent(out playerBase player)) 
    //    { 
    //        player.interactableObj = this; 
    //        //Debug.Log("player colliding with door");
    //    }
    //}
    //private void OnTriggerExit(Collider other)    {
    //    if (other.TryGetComponent(out playerBase player)) { player.interactableObj = null; } //FIX (delete??)
    //}
    public void pull(playerBase player)
    {
        if (isAccessible)
        {
            doorIsOpen = !doorIsOpen;
            if (doorIsOpen)
            {
                //play door open sound
                doorOpenSound.Play();

                animator.SetTrigger("Open");
                //Debug.Log("door open"); 
            }
            else
            {
                //play door close sound
                doorCloseSound.Play();

                animator.SetTrigger("Close");
                //Debug.Log("door closed"); 
            }
        }
        else
        {
            dialogueBase.dialogueScript.setDialogue("Can't seem to be able the door right now? I guess I'll look around", 8f);
        }
    }

    private void OnEnable()
    {
        gameplayBase.OnUnlockDoors += unlockDoor;
    }
    private void OnDisable()
    {
        gameplayBase.OnUnlockDoors -= unlockDoor;
    }

    private void unlockDoor(int doorTypeInp)
    {
        if (doorType == doorTypeInp) {
            isAccessible = true;
        }
    }
}

 