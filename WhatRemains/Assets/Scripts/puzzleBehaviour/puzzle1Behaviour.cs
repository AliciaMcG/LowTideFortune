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
    public gameplayBase puzzleManager;
    public Transform playerCam;
    public GameObject symbol;

    [Header("Measurements")]
    public float stareTime = 5f;
    public float maxDist = 12f;
    public float dist;

    [SerializeField]private float stareTimer = 0;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        puzzleManager.currPuz = 0; //Have to complete this one first
    }

    void Update()
    {
        if (puzzleManager.puzzlesCompleted[0] != true) { 
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
                    puzzleManager.completePuzzle(0);
                    Debug.Log("1 is done");
                }
            }
            else { stareTimer = 0; }
        }
    }





    //gameplayBase puzzleManager;
    //bool active = true;

    //const int totalTime = 3000;
    //float timePassed = -1;
    //public Camera playerCam;
    //public float range = 5f;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    //void Update()
    //{
    //    if (active && timePassed < totalTime)
    //    {
    //        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit, range))
    //        {
    //            if (hit.collider.CompareTag("puzzle1Symbol"))
    //            {
    //                // player watching symbol
    //                if (timePassed < 0)
    //                {
    //                    // start timer
    //                    timePassed = 0;
    //                }
    //                else
    //                {
    //                    // increase time passed
    //                    timePassed += Time.deltaTime;
    //                }
    //            }
    //            else if (timePassed > -0.5)
    //            {
    //                //reset timer
    //                timePassed = -1;
    //            }
    //        }
    //    }
    //    else if (active && timePassed >= totalTime)
    //    {
    //        // player has watched symbol for necessary time
    //        puzzleManager.completePuzzle(0);
    //    }
    //}

    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////

}
