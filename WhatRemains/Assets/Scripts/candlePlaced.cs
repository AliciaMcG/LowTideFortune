using UnityEngine;

public class candlePlaced : MonoBehaviour
{
    public gameplayBase gameplayBase;
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

        //if six candles have been placed, the game ends
        if (numCandlesPlaced == 6)
        {
            gameplayBase.completeGame();
        }
    }
}
