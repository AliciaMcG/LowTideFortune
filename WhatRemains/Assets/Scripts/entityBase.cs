using UnityEngine;

/// <summary>
/// Holds code for:
/// 
///   Entity movement
///   
///   Entity state machine:
///             0 - default 
///   
/// </summary>

public class entityBase : MonoBehaviour
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    public static entityBase entity;

    [Header("Entity Attributes")]
    public int entityState;
    public float entitySpeed;

    [Header("Objects")]
    public playerBase targetPlayer;
    Rigidbody rb; 

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
        if (entity != null && entity != this)
        {
            Destroy(gameObject);
        }
        entity = this;
        if (entity == null) { Debug.LogWarning("no entity?? "); }


    }

    void Start()
    {
        entityState = 0;
        gameObject.SetActive(false);

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        updateEntityState();
    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    private void updateEntityState()
    {
        switch (entityState)        {

            case 0:
                //nothing, entity is inactive
                break;

            case 1:
                //entity is idle
                //FIX add idle actions
                //FIX add movement
                break;

            case 2:
                //entity is messing with puzzles
                messWithPuzzle();
                break;

            case 3:
                //enity is chasing player
                //FIX start chase sounds
                entityMoveTo(targetPlayer.rb.position); 
                break;

        }
    }

    private void messWithPuzzle()
    {
        switch (gameplayBase.instance.currPuz)
        { 
            case 0:
                //
                Debug.Log("No puzzle active to mess with");
                break;

            case 1:
                //
                Debug.LogWarning("Cannot mess with puzzle one");
                break;

            case 2:
                //
                Debug.Log("Entity is messing with puzzle 2");
                break;

            case 3:
                //
                Debug.Log("Entity is messing with puzzle 3");
                break;

            case 4:
                //
                Debug.Log("Entity is messing with puzzle 4");
                break;

            case 5:
                //
                Debug.Log("Entity is messing with puzzle 5");
                break;


            default:
                Debug.LogWarning("Enitity cannot mess with an invalid puzzle index");
                break;

        }
    }

    private void entityMoveTo(Vector3 targetPOS) //FIX FOR PHYSICAL OBSTACLES
    {
        //FIX DIRECTION FACING

        rb.MovePosition((rb.position + ((targetPOS - this.rb.position).normalized) * entitySpeed * Time.fixedDeltaTime));

        // the if reaches player is in collision

    }
}
