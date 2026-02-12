using UnityEngine;

public class RitualsManager : MonoBehaviour
{
    // bools to track ritual completion
    private bool ritual1Comp = false;
    private bool ritual2Comp = false;
    private bool ritual3Comp = false;
    private bool ritual4Comp = false;
    private bool ritual5Comp = false;

    public GameObject candle1;
    public GameObject candle2;
    public GameObject candle3;
    public GameObject candle4;
    public GameObject candle5;

    public GameObject player;

    public bool ritualIsComplete(int ritualNum)
    {
        switch (ritualNum)
        {
            case 1:
                return ritual1Comp;
            case 2:
                return ritual2Comp;
            case 3:
                return ritual3Comp;
            case 4:
                return ritual4Comp;
            case 5:
                return ritual5Comp;
            default:
                return false;
        }
    }

    public void ritualSetComplete(int ritualNum)
    {
        // Set ritual complete, and activate candle for player to take

        switch (ritualNum)
        {
            case 1:
                candle1.SetActive(true);
                ritual1Comp = true;
                break;
            case 2:
                candle2.SetActive(true);
                ritual2Comp = true;
                break;
            case 3:
                candle3.SetActive(true);
                ritual3Comp = true;
                break;
            case 4:
                candle4.SetActive(true);
                ritual4Comp = true;
                break;
            case 5:
                candle5.SetActive(true);
                ritual5Comp = true;
                break;
        }
    }
}
