using UnityEngine;

/// <summary>
/// Holds code for:
/// 
///   Entity movement
///   taking player health
///   
///   Entity state machine:
///             0 - default 
///             1 - idle
///             2 - entity is messing with puzzles
///             3 - enity is chasing player
///   
/// </summary>

public class entityBase : MonoBehaviour
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    public static entityBase entity;
    public entityScriptable entityScriptable;

    [Header("Entity Attributes")]
    public int entityState;
    public float entitySpeed;
    public float messTime;
    public int currTargetPuzzle; //to compare if player actally switched rooms or just triggered the trigger objects in the same room

    [Header("Objects")]
    public playerBase targetPlayer;

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
        currTargetPuzzle = 0;
        gameObject.SetActive(false);

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        updateEntityState();

        if (messTime > 0)
        {
            if (entityState == 2) { entityState = 1; } //reset back to idle
            messTime -= Time.fixedDeltaTime;

            if (messTime <= 0)            {
                entityState = 2;
            }
        }
    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out playerBase player) && entityState == 3) {
            takePlayerHealth(player);
        }
    }

    private void OnEnable()    {
        roomTriggers.OnCurrPuzzChange += notifyEnitityOfPuzzChange;
    }
    private void OnDisable()    {
        roomTriggers.OnCurrPuzzChange -= notifyEnitityOfPuzzChange;
    }
    private void notifyEnitityOfPuzzChange()
    {
        if (gameplayBase.instance.currPuz != currTargetPuzzle)
        {
            currTargetPuzzle = gameplayBase.instance.currPuz;
            Debug.Log("Entity knows that player switched to puzzle: " + currTargetPuzzle);

            if (currTargetPuzzle == 0 || currTargetPuzzle == 1)
            {
                return;
            }
            else if (currTargetPuzzle >= 2 && currTargetPuzzle <= 4) {
                messTime = 8.5f;
            }
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
        switch (currTargetPuzzle)
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
                //FIX
                messTime = 7f;
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
                    messWithPuzz4();
                }

                break;


            default:
                Debug.LogWarning("Enitity cannot mess with an invalid puzzle index");
                break;

        }
    }

    private void entityMoveTo(Vector3 targetPOS) //FIX FOR PHYSICAL OBSTACLES
    {
        //FIX DIRECTION FACING

        transform.Translate((transform.position + ((targetPOS - this.transform.position).normalized) * entitySpeed * Time.fixedDeltaTime));

        // the if reaches player is in collision //take helth funcion

    }

    private void takePlayerHealth(playerBase player) //FIX make event
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

    public void messWithPuzz4()
    {
        for (int i = 0; i < puzzle4Behaviour.puzz4.skullsArr.Length; i++)
        {
            for (int j = 0; j < puzzle4Behaviour.puzz4.skullsArr.Length; j++)
            {

                if (Vector3.Distance(puzzle4Behaviour.puzz4.skullsArr[i].transform.position, puzzle4Behaviour.puzz4.skullPlacesArr[j].transform.position) < 3f)
                {
                    puzzle4Behaviour.puzz4.skullsArr[i].transform.position = puzzle4Behaviour.puzz4.skullPlacesArr[Random.Range(0, puzzle4Behaviour.puzz4.skullsArr.Length)].transform.position;
                    messTime = 7f;
                    break;
                }
                else { messTime = 3f; }
            }
        }

    }
}
