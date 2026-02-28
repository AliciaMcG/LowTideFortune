using UnityEngine;

public class skullsBehaviour : MonoBehaviour
{
    /// <summary>
    /// Holds code for:
    /// 
    ///   Placing skulls on pedestals
    ///   
    ///   
    ///   
    ///   
    ///   
    ///   
    ///   
    /// </summary>
    /// 
    /// 
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////

    public static bool[] skullsInPosition = new bool[3];

    public playerBase playerBaseScript;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////

    // Update is called once per frame
    void Update()
    {
        //if the ray is hitting something
        if (playerBaseScript.cast)
        {
            Debug.Log(playerBaseScript.hit.transform.name);
        
            //if the player's looking at a skull position
            if (playerBaseScript.hit.transform.CompareTag("skullPos"))
            {
                //if they're holding a skull
                if(playerBaseScript.pickedObject != null && playerBaseScript.pickedObject.name.Contains("Skull"))
                {
                    //pointing at a snapping position
                    tarotCards.pointingAtTargetPos = true;

                    Debug.Log(playerBaseScript.pickedObject);

                    //if f is pressed
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        //disconnect it from the hand and re-enable the collider
                        playerBaseScript.pickedObject.SetParent(null);

                        if (playerBaseScript.pickedObject.GetComponent<Collider>() != null) {
                            playerBaseScript.pickedObject.GetComponent<Collider>().enabled = true;
                        }

                        //snap the skull to the skull position plane
                        playerBaseScript.pickedObject.transform.position = playerBaseScript.hit.transform.position;
                    
                        //set the picked up object back to null
                        playerBaseScript.pickedObject = null;

                        //no longer holding a snap object
                        tarotCards.pointingAtTargetPos = false;
                    }
                }
            }
        }

    }
}
