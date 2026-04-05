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
        //if a skull is placed, restart puzzle 4
        if (collider.gameObject.name.Contains("Skull") && gameObject.name.Contains("Pos"))
        {
            entityBase.puzzle4Changed = true;
            Debug.Log("Puzzle changed");
        }
        //if a skull is placed in the right position
        if (collider.gameObject.name.Contains("owlSkull"))
        {
            //update the skull positions array
            skullsBehaviour.skullsInPosition[0] = true;

            //move the skull above the platform
            collider.gameObject.transform.position = new Vector3 (collider.gameObject.transform.position.x, collider.gameObject.transform.position.y + 0.3f, collider.gameObject.transform.position.z);

            if(gameObject.name.Contains("owlPos"))
            {
                entityBase.entity.entityPatience += 16f;
                //check if the skulls are in the right place
                CheckSkulls();
            }
        }
        if (collider.gameObject.name.Contains("ratSkull"))
        {
            //update the skull positions array
            skullsBehaviour.skullsInPosition[1] = true;

            //move the skull above the platform
            collider.gameObject.transform.position = new Vector3 (collider.gameObject.transform.position.x, collider.gameObject.transform.position.y + 0.1f, collider.gameObject.transform.position.z);

            if(gameObject.name.Contains("ratPos"))
            {
                entityBase.entity.entityPatience += 16f;
                //check if the skulls are in the right place
                CheckSkulls();
            }
        }
        if (collider.gameObject.name.Contains("deerSkull"))
        {
            //update the skull positions array
            skullsBehaviour.skullsInPosition[2] = true;

            //move the skull above the platform
            collider.gameObject.transform.position = new Vector3 (collider.gameObject.transform.position.x, collider.gameObject.transform.position.y + 1.0f, collider.gameObject.transform.position.z);

            if(gameObject.name.Contains("deerPos"))
            {
                entityBase.entity.entityPatience += 16f;
                CheckSkulls();
            }
            //check if the skulls are in the right place
            
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
            gameplayBase.instance.completePuzzle(4);
        }
    }
}
