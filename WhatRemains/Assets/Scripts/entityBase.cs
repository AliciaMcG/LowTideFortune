using System.Collections;
using UnityEngine;
using static entityScriptable;

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

    public bool isBeingIdle;
    public float entityPatience;

    [Header("Objects")]
    public playerBase targetPlayer;

    [Header("Room Targets")]
    public Transform room2Tar1;
    public Transform room2Tar2;
    public Transform room3Tar;
    public Transform room4Tar;

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

        entityScriptable.entitysMap = new room[11];
        for (int i = 0; i <= 11; i++) {
            entityScriptable.entitysMap[i] = new room(i);
        }

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        //if the game's not paused
        if (sceneManager.gameIsPaused == false)
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
        roomTriggers.OnCurrRoomChange += notifyEnitityOfRoomChange;
    }
    private void OnDisable()    {
        roomTriggers.OnCurrRoomChange -= notifyEnitityOfRoomChange;
    }
    private void notifyEnitityOfRoomChange()
    {
        


        //if (gameplayBase.instance.currPuz != currTargetPuzzle) //OLD
        //{
        //    currTargetPuzzle = gameplayBase.instance.currPuz;
        //    Debug.Log("Entity knows that player switched to puzzle: " + currTargetPuzzle);

        //    if (currTargetPuzzle == 0 || currTargetPuzzle == 1)
        //    {
        //        return;
        //    }
        //    else if (currTargetPuzzle >= 2 && currTargetPuzzle <= 4) {
        //        messTime = 8.5f;
        //    }
        //}

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
                //walk to room
                //speed up ig FIX
                entityState = 3; //FIX
                break;

            case 3:
                //entity is messing with puzzles
                messWithPuzzle();
                break;
            case 4:
                //enity is chasing player
                //FIX start chase sounds
                //entityMoveTo(targetPlayer.GetComponent<RigidBody>().position); 
                break;


            default:
                Debug.LogError("wrong entity state assignment");
                break;

        }
    }

    private void entityMoveTo(Vector3 targetPOS) //FIX FOR PHYSICAL OBSTACLES
    {
        //FIX DIRECTION FACING

        //entity's current position
        Vector3 currEntity = gameObject.transform.position;

        //move towards the target position
        gameObject.transform.position = Vector3.MoveTowards(currEntity, targetPOS, (float)0.01);

        //transform.Translate((transform.position + ((targetPOS - this.transform.position).normalized) * entitySpeed * Time.fixedDeltaTime));

        // the if reaches player is in collision //take helth funcion

    }

    public IEnumerator initEntityWait ()
    {
        entityPatience = 16f;
        yield return null;

        while (gameplayBase.instance.currPuz != 0)
        {
            entityPatience -= Time.fixedDeltaTime;
        }

        yield return null;
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
                //FIX
                //messTime = 7f;
                entityMoveTo(room2Tar1.position);
                entityMoveTo(room2Tar2.position);
                
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

    //public IEnumerator actIdle()
    //{
    //    int roomNum = Random.Range(0, entityScriptable.roomNumArray.Length);

    //}
}
