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

    public bool isBeingIdle;
    public bool isWaiting;
    public float entityPatience;
    public AudioSource entityFootsteps;

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

    public Transform[] targets;
    int randDest = 3;

    [Header("Paths")]
    public SplineContainer[] paths;
    public SplineAnimate splineAnimate;
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
        isWaiting = false;

    }

    void Start()
    {
        entityState = 0;
        gameObject.SetActive(false);

        /*
        entityScriptable.entitysMap = new room[11];
        for (int i = 0; i <= 11; i++) {
            entityScriptable.entitysMap[i] = new room(i);
        }
        */

    }

    void Update()
    {
        
        if (sceneManager.gameIsPaused)
        {
            if (!entityAgent.isStopped)
            {
                entityAgent.isStopped = true;
                entityAgent.velocity = Vector3.zero;
            }

            if (splineAnimate != null && splineAnimate.enabled)
            {
                splineAnimate.Pause();
                splineAnimate.enabled = false;
            }
        }
        else
        {
            if (entityAgent.isStopped)
            {
                entityAgent.isStopped = false;
            }

            if (splineAnimate != null && !splineAnimate.enabled)
            {
                splineAnimate.enabled = true;
            }
        }
        

    }

    private void FixedUpdate()
    {
        //if the game's not paused
        if (sceneManager.gameIsPaused == false)
        {
            updateEntityState();
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

    //entity stops messing with puzzle if player leaves room
    private void notifyEnitityOfRoomChange()
    {
        if ( entityPatience > 0     &&  gameplayBase.instance.player.currRoom != gameplayBase.instance.currPuz)
        {
            StartCoroutine(entityLoseInterestTimer());
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
                entityAgent.speed = 3.5f;
                entityAgent.acceleration = 1f;
                playerBase.entityChasing = false;

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
                playerBase.entityChasing = true;

                chasingTimer += Time.deltaTime;

                //chase for thirty seconds
                if (chasingTimer <= 30f)
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
                playerBase.entityChasing = false;
                
                if (entityPatience <= 0)
                {
                    splineAnimate.enabled = true;
                    messWithPuzzle();
                }

                break;

            default:
                Debug.LogError("wrong entity state assignment");
                break;

        }
    }

    //lowers patience and starts puzzle messing
    public IEnumerator initEntityWait ()
    {
        entityPatience = 16f;
        isWaiting = true;
        yield return null;

        while (entityPatience > 0 && gameplayBase.instance.currPuz != 0)
        {
            if (sceneManager.gameIsPaused == false)
            {
                entityPatience -= Time.deltaTime;
                yield return null;
            }

            //exit if paused
            yield return null;
        }
        if (entityPatience <= 0 && gameplayBase.instance.player.currRoom == gameplayBase.instance.currPuz && gameplayBase.instance.puzzlesCompleted[gameplayBase.instance.currPuz - 1] == false)
        {
            entityState = 3;
            isWaiting = false;
        }

        yield return null;
    }

    public IEnumerator entityLoseInterestTimer() 
    { 
        float timeGoneTimer = 15f;
        Debug.Log("entity knows player left");

        while (gameplayBase.instance.currPuz != 0 && (entityState == 3) && timeGoneTimer > 0 && sceneManager.gameIsPaused == false)
        {
            timeGoneTimer -= Time.deltaTime;
            Debug.Log("time left till reset: " + timeGoneTimer);
            if (gameplayBase.instance.player.currRoom == gameplayBase.instance.currPuz) {
                yield break;
            }

            yield return null;
        }
        if (timeGoneTimer < 0)
        {
            entityState = 1;
        }

        yield break;
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

                    entityAgent.SetDestination(puzzle2MessStart.transform.position);
                    if (!startedPuzzle2)
                    {
                        splineAnimate.Container = paths[0];
                        splineAnimate.Restart(true);
                        startedPuzzle2 = true;

                        thrownJar = false;
                        holdingJar = false;
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
                        startedPuzzle2 = false;
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

                        placedCard = false;
                        holdingCard = false;
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

                        //remove it from the tarot positions array
                        tarotCards.tarotsInPosition[0] = false;
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
                        startedPuzzle3 = false;
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
                                
                                if (Vector3.Distance(puzzle4Behaviour.puzz4.skullsArr[j].transform.position, puzzle4Behaviour.puzz4.skullPlacesArr[i].transform.position) < 0.6f)
                                {   
                                    numSkullsOnPed += 1;
                                    chosenSkull = j;

                                    //see which skulls are on which pedestals
                                    placedSkulls.Add(puzzle4Behaviour.puzz4.skullsArr[j]);

                                    break;

                                }
                                else 
                                { 
                                }
                            }
                            if (numSkullsOnPed == 0)
                            {
                                openPedestals.Add(puzzle4Behaviour.puzz4.skullPlacesArr[i]);
                            }
                            numSkullsOnPed = 0;
                        }

                        if (placedSkulls.Count == 0 || openPedestals.Count == 0)
                        {
                            Debug.LogError("No valid skulls or pedestals!");
                            entityState = 1;
                            return;
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

                    if (chosenSkull < 0 || chosenSkull >= placedSkulls.Count || randomPlace < 0 || randomPlace >= openPedestals.Count)
                    {
                        Debug.Log("Left because of 0");
                        entityState = 1;
                        return;
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
                    else if(splineAnimate.NormalizedTime >= 0.64 && !skullMoved && openPedestals[randomPlace] == puzzle4Behaviour.puzz4.skullPlacesArr[0])
                    {
                        placedSkulls[chosenSkull].transform.position = new Vector3 (openPedestals[randomPlace].transform.position.x, openPedestals[randomPlace].transform.position.y + 0.5f, openPedestals[randomPlace].transform.position.z);
                        skullMoved = true;
                        holdingSkull = false;
                        mess4Done = true;
                    }
                    else if(splineAnimate.NormalizedTime >= 0.75 && !skullMoved && openPedestals[randomPlace] == puzzle4Behaviour.puzz4.skullPlacesArr[1])
                    {
                        placedSkulls[chosenSkull].transform.position = new Vector3 (openPedestals[randomPlace].transform.position.x, openPedestals[randomPlace].transform.position.y + 0.5f, openPedestals[randomPlace].transform.position.z);
                        skullMoved = true;
                        holdingSkull = false;
                        mess4Done = true;
                    }
                    else if(splineAnimate.NormalizedTime >= 0.84 && !skullMoved && openPedestals[randomPlace] == puzzle4Behaviour.puzz4.skullPlacesArr[2])
                    {
                        placedSkulls[chosenSkull].transform.position = new Vector3 (openPedestals[randomPlace].transform.position.x, openPedestals[randomPlace].transform.position.y + 0.5f, openPedestals[randomPlace].transform.position.z);
                        skullMoved = true;
                        holdingSkull = false;
                        mess4Done = true;
                    }

                    //reset back to idle after messing
                    if (mess4Done == true)
                    {
                        puzzle4Changed = true;

                        openPedestals.Clear();
                        placedSkulls.Clear();

                        holdingSkull = false;
                        skullMoved = false;
                        mess4Done = false;

                        chosenSkull = -1;
                        randomPlace = -1;
                        Debug.Log("finished puzzle 4");
                        entityState = 1;
                    }
                }
                break;


            default:
                Debug.LogWarning("Entity cannot mess with an invalid puzzle index");
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
        //stop spline animation
        if (splineAnimate != null)
        {
            splineAnimate.Pause();              
            splineAnimate.enabled = false;      
        }

        //reset puzzle flags
        startedPuzzle2 = false;
        startedPuzzle3 = false;

        entityState = 2;

        //force nav mesh to entity's current position
        entityAgent.isStopped = false;
        entityAgent.ResetPath();
        entityAgent.Warp(transform.position);
    }
}
