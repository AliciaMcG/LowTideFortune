using UnityEngine;

public class candleSnapPoint : MonoBehaviour
{
    public Transform snapPosition; //position the candle should move to
    private void Reset()
    {
        snapPosition = transform;   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pickupAble"))
        {
            candleSnapPoint candle = other.GetComponent<candleSnapPoint>();
            if (candle != null)
            {
                //candle.candleSnap.SnapTo(snapPosition);
            }
        }
    }
}
