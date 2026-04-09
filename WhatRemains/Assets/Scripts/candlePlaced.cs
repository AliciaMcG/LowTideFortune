using UnityEngine;

public class candlePlaced : MonoBehaviour
{
    //entity reference
    public entityBase entity;
    public puzzle5Behaviour puzzle5Script;

    [SerializeField] public static int numCandlesPlaced = 0;

    //if a candle holder is colliding with a candle
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("candle"))
        {
            //increase the number of candles placed
            numCandlesPlaced++;
            Debug.Log("Num candles placed: " + numCandlesPlaced);

            //untag the candle so the player cant pick it up again
            other.tag = "Untagged";

            //turn the entity back to idle after candle placed
            entity.entityState = 1;

        }

        if (numCandlesPlaced == 4)
        {
            puzzle5Script.activatePuzz5();
            entity.InitChase();
        }

        //if five candles have been placed, the game ends
        if (numCandlesPlaced == 5)
        {
            gameplayBase.instance.completeGame();
        }
    }
}
