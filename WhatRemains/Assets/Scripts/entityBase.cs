using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static entityScriptable;
using UnityEngine.Splines;
using UnityEngine.AI;

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
    public float messTime;

    public bool isBeingIdle;
    public float entityPatience;

    public GameObject entityHand;

    float chasingTimer = 0f;
    float chaseUpdateTimer = 0f;
    float chaseUpdateRate = 0.2f;

    [Header("Player Health")]
    public GameObject losePanel;

    [Header("Nav Mesh")]
    UnityEngine.AI.NavMeshAgent entityAgent;
    
    public Transform puzzle2MessStart;
    public Transform puzzle3MessStart;
    public Transform puzzle4MessStart;

    bool isChasing;

    public Transform[] targets;
    int randDest = 3;

    [Header("Paths")]
    public SplineContainer[] paths;
    private SplineAnimate splineAnimate;
    bool startedPuzzle2 = false;
    bool startedPuzzle3 = false;
    public static bool puzzle4Changed = true;

    [Header("Mess Objects")]
    public GameObject jar;
    bool thrownJar = false;
    Vector3 jarPos;
    public GameObject tarotCard;
    public Transform tarotTarPos;
    bool placedCard = false;
    bool holdingCard = false;
    bool holdingJar = false;
    bool holdingSkull = false;
    bool skullMoved = false;
    int chosenSkull = -1;
    GameObject ped1Skull;
    GameObject ped2Skull;
    GameObject ped3Skull;
    int randomPlace = -1;
    int numSkullsOnPed = 0;
    bool mess4Done = false;
    List<GameObject> openPedestals = new List<GameObject>();
    List<GameObject> placedSkulls = new List<GameObject>();
  

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
        if (entity != null && entity != this)
        {
            Destroy(gameObject);
        }
        entity = this;
        if (entity == null) { Debug.LogWarning("no entity?? "); }

        //set vars
        jarPos = jar.transform.position;
        splineAnimate = GetComponent<SplineAnimate>();
        entityAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //set the entity's first destination
        entityAgent.SetDestination(targets[randDest].position);
        isChasing = false;

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
                messTime -= Time.fixedDeltaTime;

                if (messTime <= 0)            {
                    entityState = 3;
                }
            }
        }
    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out playerBase player))
        {
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
                entityAgent.speed = 3.5f;
                entityAgent.acceleration = 1f;
                
                if (!entityAgent.pathPending && entityAgent.remainingDistance < 0.5f)
                {
                    //pick a random target
                    randDest = Random.Range(0, targets.Length);

                    //set the destination to the random target
                    entityAgent.SetDestination(targets[randDest].position);

                    Debug.Log("current destination: " + targets[randDest].name);
                }

                break;

            case 2:
                //entity chases player
                entityAgent.speed = 50f;
                entityAgent.acceleration = 100f;

                chasingTimer += Time.deltaTime;

                //chase for ten seconds
                if (chasingTimer <= 10f)
                {
                    chaseUpdateTimer += Time.deltaTime;

                    //update chase destination every 0.2 seconds
                    if (chaseUpdateTimer >= chaseUpdateRate)
                    {
                        entityAgent.SetDestination(gameplayBase.instance.player.transform.position);
                        chaseUpdateTimer = 0f;
                    }
                }
                else
                {
                    chasingTimer = 0f;
                    entityState = 1;
                }
                break;

            case 3:
                //entity is messing with puzzles
                entityAgent.speed = 3.5f;
                entityAgent.acceleration = 1f;
                
                messWithPuzzle();
                break;

            default:
                Debug.LogError("wrong entity state assignment");
                break;

        }
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
                if (gameplayBase.instance.puzzlesCompleted[1] == false)
                {
                //FIX
                //messTime = 7f;

                    entityAgent.SetDestination(puzzle2MessStart.transform.position);
                    if (!startedPuzzle2)
                    {
                        splineAnimate.Container = paths[0];
                        splineAnimate.Play();
                        startedPuzzle2 = true;
                    }

                    //throw the jar
                    if (splineAnimate.NormalizedTime >= 0.97 && thrownJar == false)
                    {
                        jar.transform.position = jarPos;
                        Debug.Log("threw jar");
                        thrownJar = true;
                        holdingJar = false;
                    }
                    //grab the jar when it reaches it
                    else if (splineAnimate.NormalizedTime >= 0.87  && holdingJar == false && thrownJar == false)
                    {
                        jar.transform.position = entityHand.transform.position;
                        holdingJar = true;
                        Debug.Log("grabbed jar");
                    }
                    if (holdingJar && !thrownJar)
                    {
                        jar.transform.position = entityHand.transform.position;
                    }

                    //reset back to idle after messing
                    if (thrownJar == true)
                    {
                        entityState = 1;
                    }
                }
                break;

            case 3:
                //
                Debug.Log("Entity is messing with puzzle 3");
                if (gameplayBase.instance.puzzlesCompleted[2] == false)
                {
                    entityAgent.SetDestination(puzzle3MessStart.transform.position);
                    if (!startedPuzzle3)
                    {
                        splineAnimate.Container = paths[1];
                        splineAnimate.Restart(true);
                        startedPuzzle3 = true;
                    }

                    //place the tarot card
                    if (splineAnimate.NormalizedTime >= 1 && placedCard == false)
                    {
                        holdingCard = false;
                        tarotCard.transform.position = tarotTarPos.position;
                        Debug.Log("moved card");
                        placedCard = true;
                    }
                    //grab the tarot card when it reaches it
                    else if (splineAnimate.NormalizedTime >= 0.67 && holdingCard == false && placedCard == false)
                    {
                        tarotCard.transform.position = entityHand.transform.position;
                        holdingCard = true;
                        Debug.Log("grabbed card");
                    }
                    
                    if (holdingCard && !placedCard)
                    {
                        tarotCard.transform.position = entityHand.transform.position;
                        Debug.Log("holding card");
                    }

                    //reset back to idle after messing
                    if (placedCard == true)
                    {
                        entityState = 1;
                    }
                }
                
                break;

            case 4:
                // Skulls
                Debug.Log("Entity is messing with puzzle 4");
                entityAgent.SetDestination(puzzle4MessStart.transform.position);

                if (gameplayBase.instance.puzzlesCompleted[3] == false)
                {
                    if (puzzle4Changed && !mess4Done)
                    {
                        puzzle4Changed = false;
                        openPedestals.Clear();
                        placedSkulls.Clear();

                        splineAnimate.Container = paths[2];
                        splineAnimate.Restart(true);

                        for (int i = 0; i < puzzle4Behaviour.puzz4.skullPlacesArr.Length; i++)
                        {
                            for (int j = 0; j < puzzle4Behaviour.puzz4.skullsArr.Length; j++)
                            {
                                Debug.Log("In position and skull check");
                                Debug.Log("skull and position distance: " + Vector3.Distance(puzzle4Behaviour.puzz4.skullsArr[i].transform.position, puzzle4Behaviour.puzz4.skullPlacesArr[j].transform.position));
                                
                                if (Vector3.Distance(puzzle4Behaviour.puzz4.skullsArr[j].transform.position, puzzle4Behaviour.puzz4.skullPlacesArr[i].transform.position) < 0.3f)
                                {   
                                    numSkullsOnPed += 1;
                                    chosenSkull = j;
                                    messTime = 7f;

                                    //see which skulls are on which pedestals
                                    placedSkulls.Add(puzzle4Behaviour.puzz4.skullsArr[j]);

                                    break;

                                }
                                else 
                                { 
                                    messTime = 3f; 
                                }
                            }
                            if (numSkullsOnPed == 0)
                            {
                                openPedestals.Add(puzzle4Behaviour.puzz4.skullPlacesArr[i]);
                            }
                            numSkullsOnPed = 0;
                        }

                        //only move to a pedestal thats not its own
                        randomPlace = Random.Range(0, openPedestals.Count);
                        chosenSkull = Random.Range(0, placedSkulls.Count);

                        Debug.Log("random place: " + randomPlace);
                    }
                
                
                    foreach (var pedestal in openPedestals)
                    {
                        Debug.Log("Pedestal: " + pedestal.name);
                    }
                    
                    if (holdingSkull && !skullMoved)
                    {
                        placedSkulls[chosenSkull].transform.position = entityHand.transform.position;
                    }
                    if(splineAnimate.NormalizedTime >= 0.28 && placedSkulls[chosenSkull] == puzzle4Behaviour.puzz4.skullsArr[0] && holdingSkull == false && skullMoved == false)
                    {
                        placedSkulls[chosenSkull].transform.position = entityHand.transform.position;
                        holdingSkull = true;
                    }
                    else if(splineAnimate.NormalizedTime >= 0.39 && placedSkulls[chosenSkull] == puzzle4Behaviour.puzz4.skullsArr[1]  && holdingSkull == false && skullMoved == false)
                    {
                        placedSkulls[chosenSkull].transform.position = entityHand.transform.position;
                        holdingSkull = true;
                    }
                    else if(splineAnimate.NormalizedTime >= 0.46 && placedSkulls[chosenSkull] == puzzle4Behaviour.puzz4.skullsArr[2]  && holdingSkull == false && skullMoved == false)
                    {
                        placedSkulls[chosenSkull].transform.position = entityHand.transform.position;
                        holdingSkull = true;
                    }
                    //put the skull on the pedestal as it walks by
                    else if(splineAnimate.NormalizedTime >= 0.64 && splineAnimate.NormalizedTime < 0.65 && openPedestals[randomPlace] == puzzle4Behaviour.puzz4.skullPlacesArr[0])
                    {
                        placedSkulls[chosenSkull].transform.position = new Vector3 (openPedestals[randomPlace].transform.position.x, openPedestals[randomPlace].transform.position.y + 1, openPedestals[randomPlace].transform.position.z);
                        skullMoved = true;
                        holdingSkull = false;
                        mess4Done = true;
                    }
                    else if(splineAnimate.NormalizedTime >= 0.75 && splineAnimate.NormalizedTime < 0.76 && openPedestals[randomPlace] == puzzle4Behaviour.puzz4.skullPlacesArr[1])
                    {
                        placedSkulls[chosenSkull].transform.position = new Vector3 (openPedestals[randomPlace].transform.position.x, openPedestals[randomPlace].transform.position.y + 1, openPedestals[randomPlace].transform.position.z);
                        skullMoved = true;
                        holdingSkull = false;
                        mess4Done = true;
                    }
                    else if(splineAnimate.NormalizedTime >= 0.84 && splineAnimate.NormalizedTime < 0.85 && openPedestals[randomPlace] == puzzle4Behaviour.puzz4.skullPlacesArr[2])
                    {
                        placedSkulls[chosenSkull].transform.position = new Vector3 (openPedestals[randomPlace].transform.position.x, openPedestals[randomPlace].transform.position.y + 1, openPedestals[randomPlace].transform.position.z);
                        skullMoved = true;
                        holdingSkull = false;
                        mess4Done = true;
                    }

                    //reset back to idle after messing
                    if (mess4Done == true)
                    {
                        entityState = 1;
                    }
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
            sceneManager.gameIsPaused = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            losePanel.SetActive(true);
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

    public void InitChase()
    {
        entityState = 2;
    }
    /*
    public void messWithPuzz4()
    {
        Debug.Log("Messing with puzzle 4 function");
        for (int i = 0; i < puzzle4Behaviour.puzz4.skullsArr.Length; i++)
        {
            for (int j = 0; j < puzzle4Behaviour.puzz4.skullsArr.Length; j++)
            {
                Debug.Log("In position and skull check");
                Debug.Log("skull and position distance: " + Vector3.Distance(puzzle4Behaviour.puzz4.skullsArr[i].transform.position, puzzle4Behaviour.puzz4.skullPlacesArr[j].transform.position));
                if (Vector3.Distance(puzzle4Behaviour.puzz4.skullsArr[i].transform.position, puzzle4Behaviour.puzz4.skullPlacesArr[j].transform.position) < 0.3f && skullMoved == false)
                {
                    int randomPlace = Random.Range(0, puzzle4Behaviour.puzz4.skullsArr.Length);
                    
                    //only move to a pedestal thats not its own
                    do
                    {
                        puzzle4Behaviour.puzz4.skullsArr[i].transform.position = puzzle4Behaviour.puzz4.skullPlacesArr[randomPlace].transform.position;
                    }
                    while (randomPlace == i);

                    skullMoved = true;
                    Debug.Log("Moved skulls");
                    messTime = 7f;
                    break;
                }
                else { messTime = 3f; }
            }
        }
    
    }
    */

    //public IEnumerator actIdle()
    //{
    //    int roomNum = Random.Range(0, entityScriptable.roomNumArray.Length);

    //}
}
