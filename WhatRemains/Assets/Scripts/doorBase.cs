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
    public bool doorIsInteractible;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
    }

    void Start()
    {
        doorIsOpen = false;
        doorIsInteractible = false;
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {

    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        doorIsInteractible = true;
    //        //if (sdfsdf) // HOW????
    //        //{

    //        //}
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        doorIsInteractible = true;

    //    }
    //}
}
