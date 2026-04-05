using UnityEngine;

public class snapInteractable : MonoBehaviour,  IInteractable
{
    
    public void interact(playerBase player)
    {
        //if an object is held
        if (gameplayBase.instance.player.pickedObject != null)
        {
            //disconnect it from the hand and re-enable the collider
            gameplayBase.instance.player.pickedObject.SetParent(null);

            if (gameplayBase.instance.player.pickedObject.GetComponent<Collider>() != null)
            {
                gameplayBase.instance.player.pickedObject.GetComponent<Collider>().enabled = true;
            }

            //snap the object to the position plane
            gameplayBase.instance.player.pickedObject.transform.position = gameplayBase.instance.player.hit.transform.position;

            //set the picked up object back to null
            gameplayBase.instance.player.pickedObject = null;

            //no longer holding a snap object
            pointAtTarget.pointingAtTargetPos = false;
        }

        //update curr puzz
        if (this.GetComponent<candleID>() == null) {
            int newCurrPuzz = (gameplayBase.instance.player.currRoom != 6) ? gameplayBase.instance.player.currRoom : 3;

            if (newCurrPuzz != gameplayBase.instance.currPuz)
            {
                gameplayBase.instance.updateCurrPuzz(newCurrPuzz);
            }
        }
    }
}
