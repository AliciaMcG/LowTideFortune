using UnityEngine;

/// <summary>
/// Holds code for:
/// 
///   Checking if the player is in the final safe room
///   
///
///   
/// </summary>

public class finalRoomTrigger : MonoBehaviour
{
    public static bool playerEnteredFinalSR;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerEnteredFinalSR = true;
            //Debug.Log("Entered final room");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerEnteredFinalSR = false;
        }
    }
}
