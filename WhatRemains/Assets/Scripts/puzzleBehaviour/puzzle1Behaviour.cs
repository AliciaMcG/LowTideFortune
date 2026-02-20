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
    public GameObject room1Locks;

    [Header("Measurements")]
    public float stareTime = 5f;
    public float maxDist = 12f;
    public float dist;

    [SerializeField] private float stareTimer = 0;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        puzzleManager.currPuz = 0; //Have to complete this one first
        room1Locks.SetActive(true);
    }

    void Update()
    {
        if (puzzleManager.puzzlesCompleted[0] != true)
        {
            dist = Vector3.Distance(playerCam.position, symbol.transform.position);
            if (dist <= maxDist)
            {
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
                    puzzleManager.entityIsActive = true;
                    room1Locks.SetActive(false);
                    Debug.Log("1 is done, entity released");
                }
            }
            else { stareTimer = 0; }
        }
    }



}