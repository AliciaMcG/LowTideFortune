using System.Diagnostics;
using UnityEngine;
//for the cult room specifically

public class candlePlacement : MonoBehaviour
{
    public int placementID;
    public Transform snapPoint;

    private void OnTriggerEnter(Collider other)
    {
        int candle = other.GetComponent<candleID>().candlesID;
        UnityEngine.Debug.Log("candleID" + candle);
        UnityEngine.Debug.Log("placementID" + placementID);

        if (other == null)
        {
            return;
        }

        if (candle != placementID)
        {
            return;
        }

        SnapCandle(other.GetComponent<candleID>());
    }

    void SnapCandle(candleID candle)
    {
        candle.transform.position = snapPoint.position;
        candle.transform.rotation = snapPoint.rotation;

        Rigidbody rb = candle.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        gameObject.SetActive(false);
    }
}
