using UnityEngine;

public class dumpCauldronButton : MonoBehaviour
{
    public float pressDistance = 0.1f;
    public float pressSpeed = 3f;

    public puzzle2Behaviour puzzle;

    Vector3 startPos;
    bool isPressed = false;
    bool isAnimating = false;

    void Start()
    {

        startPos = transform.localPosition;

    }

    void Update()
    {

        Vector3 target = isPressed ? startPos - Vector3.up * pressDistance : startPos;

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, pressSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, target) < 0.001f)
        {
            if (isPressed)
            {
                isPressed = false;
            }
            else
            {
                isAnimating = false;
            }
        }

    }

    public void Press()
    {

        if(puzzle != null)
        {
            puzzle.dumpMixture();
        }

        isPressed = true;
        isAnimating = true;

    }
}
