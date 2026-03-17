using UnityEngine;

/// <summary>
/// Holds code for:
/// 
///   pickup code for stuff like
///         
///         candles
///         tarot cards
///         jars
///   
/// </summary>

public class pickupInteractable : MonoBehaviour,  IInteractable
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    public void interact(playerBase player)
    {
        player.pickedObject = this.transform;

        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<Collider>().enabled = false;

        Debug.Log("Picked up: " + this.transform.name);
    }
    public void undoPickup(playerBase player)
    {

        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Collider>().enabled = true;

        player.pickedObject = null;

        Debug.Log("Dropped: " + this.transform.name);
    }
}
