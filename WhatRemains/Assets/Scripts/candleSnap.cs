using UnityEngine;

public class candleSnap : MonoBehaviour
{
    public void SnapTo(Transform target)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        transform.position = target.position;
        transform.rotation = target.rotation;

        transform.SetParent(target);
    }

    
}
