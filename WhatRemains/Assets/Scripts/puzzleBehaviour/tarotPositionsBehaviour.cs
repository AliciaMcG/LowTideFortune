using UnityEngine;

public class tarotPositionsBehaviour : MonoBehaviour
{
    /// <summary>
    /// Holds code for:
    /// 
    ///   Checking if the tarot cards are placed in the right order
    ///   completing puzzle index 2 (spawning candle)
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
    /// 

    void OnTriggerEnter(Collider collider)
    {
        //if a card is placed in the right position
        if (collider.gameObject.name.Contains("aquariusTarotCard") && gameObject.name.Contains("Tarot Position 1"))
        {
            entityBase.entity.entityPatience += 16f;
            //update the tarot positions array
            tarotCards.tarotsInPosition[0] = true;
            //Debug.Log(string.Join(", ", tarotCards.tarotsInPosition));
            CheckCards();
        }
        if (collider.gameObject.name.Contains("picesTarotCard") && gameObject.name.Contains("Tarot Position 2"))
        {
            entityBase.entity.entityPatience += 16f;
            //update the tarot positions array
            tarotCards.tarotsInPosition[1] = true;
            //Debug.Log(string.Join(", ", tarotCards.tarotsInPosition));
            CheckCards();
        }
        if (collider.gameObject.name.Contains("ariesTarotCard") && gameObject.name.Contains("Tarot Position 3"))
        {
            entityBase.entity.entityPatience += 16f;
            //update the tarot positions array
            tarotCards.tarotsInPosition[2] = true;
            //Debug.Log(string.Join(", ", tarotCards.tarotsInPosition));
            CheckCards();
        }
        if (collider.gameObject.name.Contains("taurusTarotCard") && gameObject.name.Contains("Tarot Position 4"))
        {
            entityBase.entity.entityPatience += 16f;
            //update the tarot positions array
            tarotCards.tarotsInPosition[3] = true;
            //Debug.Log(string.Join(", ", tarotCards.tarotsInPosition));
            CheckCards();
        }
        if (collider.gameObject.name.Contains("geminiTarotCard") && gameObject.name.Contains("Tarot Position 5"))
        {
            entityBase.entity.entityPatience += 16f;
            //update the tarot positions array
            tarotCards.tarotsInPosition[4] = true;
            //Debug.Log(string.Join(", ", tarotCards.tarotsInPosition));
            CheckCards();
        }
        if (collider.gameObject.name.Contains("cancerTarotCard") && gameObject.name.Contains("Tarot Position 6"))
        {
            entityBase.entity.entityPatience += 16f;
            //update the tarot positions array
            tarotCards.tarotsInPosition[5] = true;
            //Debug.Log(string.Join(", ", tarotCards.tarotsInPosition));
            CheckCards();
        }
        if (collider.gameObject.name.Contains("leoTarotCard") && gameObject.name.Contains("Tarot Position 7"))
        {
            entityBase.entity.entityPatience += 16f;
            //update the tarot positions array
            tarotCards.tarotsInPosition[6] = true;
            //Debug.Log(string.Join(", ", tarotCards.tarotsInPosition));
            CheckCards();
        }
        if (collider.gameObject.name.Contains("virgoTarotCard") && gameObject.name.Contains("Tarot Position 8"))
        {
            entityBase.entity.entityPatience += 16f;
            //update the tarot positions array
            tarotCards.tarotsInPosition[7] = true;
            //Debug.Log(string.Join(", ", tarotCards.tarotsInPosition));
            CheckCards();
        }
        if (collider.gameObject.name.Contains("libraTarotCard") && gameObject.name.Contains("Tarot Position 9"))
        {
            entityBase.entity.entityPatience += 16f;
            //update the tarot positions array
            tarotCards.tarotsInPosition[8] = true;
            //Debug.Log(string.Join(", ", tarotCards.tarotsInPosition));
            CheckCards();
        }
        if (collider.gameObject.name.Contains("scorpioTarotCard") && gameObject.name.Contains("Tarot Position 10"))
        {
            entityBase.entity.entityPatience += 16f;
            //update the tarot positions array
            tarotCards.tarotsInPosition[9] = true;
            //Debug.Log(string.Join(", ", tarotCards.tarotsInPosition));
            CheckCards();
        }
        if (collider.gameObject.name.Contains("sagittariusTarotCard") && gameObject.name.Contains("Tarot Position 11"))
        {
            entityBase.entity.entityPatience += 16f;
            //update the tarot positions array
            tarotCards.tarotsInPosition[10] = true;
            //Debug.Log(string.Join(", ", tarotCards.tarotsInPosition));
            CheckCards();
        }
        if (collider.gameObject.name.Contains("capricornTarotCard") && gameObject.name.Contains("Tarot Position 12"))
        {
            entityBase.entity.entityPatience += 16f;
            //update the tarot positions array
            tarotCards.tarotsInPosition[11] = true;
            //Debug.Log(string.Join(", ", tarotCards.tarotsInPosition));
            CheckCards();
        }
    }

    void CheckCards()
    {
        int numCards = 0;

        //check if the cards are in the right order
        foreach (bool card in tarotCards.tarotsInPosition)
        {
            if (card == true)
            {
                numCards++;
            }
            //Debug.Log(numCards);
        }

        //spawn candle if they are and set puzzle to complete
        if (numCards == 12)
        {
            gameplayBase.instance.completePuzzle(3);
        }
    }
}
