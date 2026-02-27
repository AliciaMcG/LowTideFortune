using UnityEngine;

public class tarotCards : MonoBehaviour
{
    /// <summary>
    /// Holds code for:
    /// 
    ///   Placing tarot cards on the table
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

    public static bool[] tarotsInPosition = new bool[12];

    public playerBase playerBaseScript;

    public static bool pointingAtTargetPos;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////

    // Update is called once per frame
    void Update()
    {
        //if the ray is hitting something
        if (playerBaseScript.cast)
        {
            Debug.Log(playerBaseScript.hit.transform.name);
        
            //if the player's looking at a tarot position
            if (playerBaseScript.hit.transform.CompareTag("tarotPos"))
            {
                //if they're holding a tarot card
                if(playerBaseScript.pickedObject != null && playerBaseScript.pickedObject.name.Contains("TarotCard"))
                {
                    //pointing at a snapping position
                    pointingAtTargetPos = true;

                    //Debug.Log(playerBaseScript.pickedObject);

                    //if f is pressed
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        //disconnect it from the hand and re-enable the collider
                        playerBaseScript.pickedObject.SetParent(null);

                        if (playerBaseScript.pickedObject.GetComponent<Collider>() != null) {
                            playerBaseScript.pickedObject.GetComponent<Collider>().enabled = true;
                        }

                        //snap the card to the card position plane
                        playerBaseScript.pickedObject.transform.position = playerBaseScript.hit.transform.position;
                    
                        //set the picked up object back to null
                        playerBaseScript.pickedObject = null;

                        //no longer holding a snap object
                        pointingAtTargetPos = false;
                    }
                }
            }
        }

    }
}
