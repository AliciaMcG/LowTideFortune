using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Holds code for:
/// 
///   ///
///   
/// </summary>

public class doorBase : MonoBehaviour
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out playerBase player)) { player.interactableObj = gameObject; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out playerBase player)) { player.interactableObj = null; }
    }
}

 