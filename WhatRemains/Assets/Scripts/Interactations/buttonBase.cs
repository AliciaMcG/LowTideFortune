using UnityEngine;

/// <summary>
/// Holds code for:
/// 
///   button for PUZZLE 2
///   
/// </summary>

public class buttonBase : MonoBehaviour, IInteractable
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    [Header("Button Animation Consts")]
    public float pressDistance = 0.1f;
    public float pressSpeed = 3f;

    [Header("Button Animation")]
    Vector3 startPos;
    bool isPressed = false;
    bool isAnimating = false;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
    }

    void Start()
    {
        startPos = transform.localPosition;
        isAnimating = false;
    }

    void Update()
    {
        if (isAnimating)
        {
            Vector3 target = isPressed ? startPos - Vector3.up * pressDistance : startPos;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, pressSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.localPosition, target) < 0.001f)            {

                if (isPressed)                {
                    isPressed = false;
                }
                else                {
                    isAnimating = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    public void interact(playerBase player)
    {
        pressButton();
        //Debug.Log("Pressed Button");
    }

    public void pressButton()
    {
        if (puzzle2Behaviour.puzz2.currentIngredients != null)
        {
            puzzle2Behaviour.puzz2.dumpMixture();
            Debug.Log("Dumped mixture");
        }
        else { Debug.Log("Nothing to dump"); }

            isPressed = true;
        isAnimating = true;

    }
}
