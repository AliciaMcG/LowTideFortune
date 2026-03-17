using UnityEngine;

/// <summary>
/// Holds code for:
/// 
///   ///
///   
/// </summary>

public class puzzle4Behaviour : MonoBehaviour
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    public static puzzle4Behaviour puzz4;
    public GameObject[] skullsArr = new GameObject[3];
    public GameObject[] skullPlacesArr = new GameObject[3];


    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
        if (puzz4 != null && puzz4 != this)
        {
            Destroy(gameObject);
        }
        puzz4 = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (entityBase.entity.messTime > 0)
        {
            entityBase.entity.messTime -= Time.fixedDeltaTime;
        }
    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    private void OnTriggerEnter(Collider other)
    {
        if (gameplayBase.instance.puzzlesCompleted[3] != true)
        {
            gameplayBase.instance.currPuz = 4;
        }
        
    } 
}
