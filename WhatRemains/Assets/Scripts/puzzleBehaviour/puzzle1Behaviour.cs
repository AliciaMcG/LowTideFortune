using UnityEngine;

/// <summary>
/// Holds code for:
/// 
///   Puzzle index 0
///   
/// </summary>

public class puzzle1Behaviour : MonoBehaviour
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    [Header("Objects")]
    public Transform playerCam;
    public GameObject symbol;

    [Header("Measurements")]
    public float stareTime = 5f;
    public float maxDist = 12f;
    public float dist;

    private float stareTimer = 0;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        gameplayBase.instance.currPuz = 0; //Have to complete this one first
    }

    void Update()
    {
        if (gameplayBase.instance.puzzlesCompleted[0] != true) { 
            dist = Vector3.Distance(playerCam.position, symbol.transform.position);
            if (dist <= maxDist) {
                checkStare();
            }
            else { stareTimer = 0; }
        }
        

    }

    private void FixedUpdate()
    {
    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    private void checkStare()
    {
        Ray ray = new Ray(playerCam.position, playerCam.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDist))
        {
            if (hit.collider.gameObject == symbol)
            {
                stareTimer += Time.deltaTime;

                if (stareTimer >= stareTime)
                {
                    gameplayBase.instance.completePuzzle(0);
                    Debug.Log("1 is done");
                }
            }
            else { stareTimer = 0; }
        }
    }
}
