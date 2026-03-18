using UnityEngine;

public class ingredientJar : MonoBehaviour
{
    /// <summary>
    /// Holds code for:
    /// 
    ///   Ingredient id to check validity
    ///   correct ingredients:
    ///     snake skin, jarID = 0
    ///     mushrooms,  jarID = 1
    ///     teeth,      jarID = 2
    ///     blood,      jarID = 3
    ///     other,      jarID = 4
    ///   
    /// </summary>
    /// 

    public int id;
    Vector3 startPos;
    Quaternion startRotation;

    Rigidbody rb;

    void Start()
    {
        startPos = transform.position;
        startRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
    }

    public void RespawnJar()
    {
        transform.position = startPos;
        transform.rotation = startRotation;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

}
