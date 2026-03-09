using UnityEngine;

//snapping the candle to he point of their placement in the cult room
public class candleSnapPoint : MonoBehaviour
{
    public int placementID;
    public Transform snapPoint;

    private void OnTriggerEnter(Collider other)
    {
        CandleID candle = other.GetComponent<CandleID>();

        if (candle == null)
        {
            return;
        }

        if (candle.candleID != placementID)
        {
            return;
        }

        SnapCandle(candle);
    }

    void SnapCandle(CandleID candle)
    {
        candle.transform.position = transform.position;
        candle.transform.rotation = transform.rotation;

        Rigidbody rb = candle.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        gameObject.SetActive(false);
    }

}
