using UnityEngine;

public class snapInteractable : MonoBehaviour,  IInteractable
{
    public playerBase playerBaseScriptDT;
    public playerBase playerBaseScriptVR;
    public playerBase playerBaseScript;
    
    public void interact(playerBase player)
    {
        if (playerBase.desktopMode) { playerBaseScript = playerBaseScriptDT; }
        else { playerBaseScript = playerBaseScriptVR; }

        //if an object is held
        if (playerBaseScript.pickedObject != null)
        {
            //disconnect it from the hand and re-enable the collider
            playerBaseScript.pickedObject.SetParent(null);

            if (playerBaseScript.pickedObject.GetComponent<Collider>() != null)
            {
                playerBaseScript.pickedObject.GetComponent<Collider>().enabled = true;
            }

            //snap the card to the card position plane
            playerBaseScript.pickedObject.transform.position = playerBaseScript.hit.transform.position;

            //set the picked up object back to null
            playerBaseScript.pickedObject = null;

            //no longer holding a snap object
            pointAtTarget.pointingAtTargetPos = false;
        }
    }
}
