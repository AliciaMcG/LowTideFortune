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
    public float messTime;

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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out playerBase player) && entityState == 3) {
            takePlayerHealth(player);
        }
    }

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
                if (messTime <= 0)
                {
                    entityState = 2;
                }
                break;

            case 2:
                //entity is messing with puzzles
                messWithPuzzle();
                break;

            case 3:
                //enity is chasing player
                //FIX start chase sounds
                //entityMoveTo(targetPlayer.GetComponent<RigidBody>().position); 
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
                // Skulls
                Debug.Log("Entity is messing with puzzle 4");

                if (gameplayBase.instance.puzzlesCompleted[3] == false)
                {

                    for (int i = 0; i < puzzle4Behaviour.puzz4.skullsArr.Length; i++)
                    {
                        for (int j = 0; j < puzzle4Behaviour.puzz4.skullsArr.Length; j++) { 

                            if (Vector3.Distance(puzzle4Behaviour.puzz4.skullsArr[i].transform.position, puzzle4Behaviour.puzz4.skullPlacesArr[j].transform.position) < 3f) {
                                puzzle4Behaviour.puzz4.skullsArr[i].transform.position = puzzle4Behaviour.puzz4.skullPlacesArr[Random.Range(0, puzzle4Behaviour.puzz4.skullsArr.Length)].transform.position;
                                messTime = 7f;
                                break;
                            }
                            else { messTime = 3f; }                        
                        }
                    }
                }

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

    private void takePlayerHealth(playerBase player)
    {
        player.playerHealth--;

        if (player.playerHealth <= 0)
        {
            sceneManager.gameOver = true;
        }

        //Set display
        for (int i = 0; i < gameplayBase.instance.healthDisplay.Length; i++)
        {
            if (i <= player.playerHealth)
            {
                gameplayBase.instance.healthDisplay[i].sprite = gameplayBase.instance.healthTrue;
            }
            else if (i > player.playerHealth)
            {
                gameplayBase.instance.healthDisplay[i].sprite = gameplayBase.instance.healthFalse;
            }
        }
    }
}
