using UnityEngine;

public class pointAtTarget : MonoBehaviour
{
    public static bool pointingAtTargetPos;
    public playerBase playerBaseScript;

    void Update()
    {
        //if the ray is hitting something
        if (playerBaseScript.cast)
        {
            //Debug.Log(playerBaseScript.hit.transform.name);
        
            //if the player's looking at a snap position
            if (playerBaseScript.hit.collider.GetComponent<snapInteractable>() != null)
            {
                //if they're holding a tarot card or a skull
                if(playerBaseScript.pickedObject != null && (playerBaseScript.pickedObject.name.Contains("TarotCard") || playerBaseScript.pickedObject.name.Contains("Skull")))
                {
                    //pointing at a snapping position
                    pointingAtTargetPos = true;

                    //Debug.Log(playerBaseScript.pickedObject);
                }
            }
        }
    }
}
