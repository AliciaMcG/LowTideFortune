using UnityEngine;

public class candlePlaced : MonoBehaviour
{
    public static int numCandlesPlaced = 0;

    //if a candle holder is colliding with a candle
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("candle"))
        {
            //increase teh number of candles placed
            numCandlesPlaced++;

            //untag the candle so the player cant pick it up again
            other.tag = "Untagged";

        }

        //if five candles have been placed, the game ends
        if (numCandlesPlaced == 5)
        {
            gameplayBase.instance.completeGame();
        }
    }
}
