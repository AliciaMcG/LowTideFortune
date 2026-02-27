using UnityEngine;

public class skullPositionBehaviour : MonoBehaviour
{
    /// <summary>
    /// Holds code for:
    /// 
    ///   Checking if the skulls cards are on the right pedestal
    ///   completing puzzle index 3 (spawning candle)
    ///   
    ///   
    ///   
    ///   
    ///   
    ///   
    /// </summary>
    /// 
    /// 
    ///////////////////////////////////////////////////////////      LOOPS      ////////////////////////////////////////////////////////////////////////////////

    void OnTriggerEnter(Collider collider)
    {
        //if a card is placed in the right position
        if (collider.gameObject.name.Contains("owlSkull") && gameObject.name.Contains("owlPos"))
        {
            //update the skull positions array
            skullsBehaviour.skullsInPosition[0] = true;

            //move the skull above the platform
            collider.gameObject.transform.position = new Vector3 (collider.gameObject.transform.position.x, collider.gameObject.transform.position.y + 0.3f, collider.gameObject.transform.position.z);

            //check if the skulls are in the right place
            CheckSkulls();
        }
        if (collider.gameObject.name.Contains("ratSkull") && gameObject.name.Contains("ratPos"))
        {
            //update the skull positions array
            skullsBehaviour.skullsInPosition[1] = true;

            //move the skull above the platform
            collider.gameObject.transform.position = new Vector3 (collider.gameObject.transform.position.x, collider.gameObject.transform.position.y + 0.1f, collider.gameObject.transform.position.z);

            //check if the skulls are in the right place
            CheckSkulls();
        }
        if (collider.gameObject.name.Contains("deerSkull") && gameObject.name.Contains("deerPos"))
        {
            //update the skull positions array
            skullsBehaviour.skullsInPosition[2] = true;

            //move the skull above the platform
            collider.gameObject.transform.position = new Vector3 (collider.gameObject.transform.position.x, collider.gameObject.transform.position.y + 1.0f, collider.gameObject.transform.position.z);

            //check if the skulls are in the right place
            CheckSkulls();
        }
        
    }

    void CheckSkulls()
    {
        int numSkulls = 0;

        //check if the skulls are in the right place
        foreach (bool skull in skullsBehaviour.skullsInPosition)
        {
            if (skull == true)
            {
                numSkulls++;
            }
            Debug.Log("numSkulls:" + numSkulls);
        }

        //spawn candle if they are and set puzzle to complete
        if (numSkulls == 3)
        {
            gameplayBase.instance.completePuzzle(3);
        }
    }
}
