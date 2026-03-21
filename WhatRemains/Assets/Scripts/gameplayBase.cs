using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Holds code for:
/// 
///   Global variables 
/// 
///   Ending the game
///   
///   Placed this script on candlesFolder
///   
///   !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! current puzzle STARTS FROM 1 TO 5 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
///   0 for none
///   
///
///   
/// </summary>

public class gameplayBase : MonoBehaviour
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    public static gameplayBase instance;
    
    [Header("Progress Tracking")]
    public bool entityIsSpawned;
    public bool diningUnlocked;
    public bool[] puzzlesCompleted; // track completion
    public int numPuzzlesCompleted;
    public int currPuz; //current puzzle

    [Header("UI")]
    public Sprite healthTrue;
    public Sprite healthFalse;

    public Image[] healthDisplay = new Image[3];

    [Header("Objects")]
    public GameObject[] candlesArr;
    public GameObject[] candlePlacements;
    public AudioSource candleSpawnSound;
    public AudioSource entityScream;
    public ParticleSystem entityParticles;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;


        puzzlesCompleted = new bool[] { false, false, false, false, false };
        currPuz = 1;
    }

    void Start()
    {
        foreach (GameObject candle  in candlesArr) {
            candle.SetActive(false);
        }

        foreach (GameObject candlePlacement in candlePlacements)
        {
            candlePlacement.SetActive(false);
        }

        entityIsSpawned = false;
        numPuzzlesCompleted = 0;
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {

    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    public void completePuzzle(int currPuzInt)    // skulls are in the correct placement
    {
        int correctedIndex = currPuzInt - 1;
        if (correctedIndex >= 0 && correctedIndex <= 4)
        {
            puzzlesCompleted[correctedIndex] = true;

            // set active curr candle :)
            candlesArr[correctedIndex].SetActive(true);
            candlePlacements[correctedIndex].SetActive(true);

            //play the candle spawn sound
            candleSpawnSound.Play();

            //increase the number of puzzles completed
            numPuzzlesCompleted++; 
        }
    }

    public void completeGame()
    {
        //entity screams //FIX
        if (!entityScream.isPlaying)
        {
            entityScream.Play();
        }
        entityParticles.Play();
    }

    public void spawnEntity()
    {
        entityBase.entity.gameObject.SetActive(true); //activates entity object
        entityBase.entity.entityState = 1;
        dialogueBase.dialogueScript.setDialogue("Wh-what is THAT???", 3f);
        entityIsSpawned = true;
        unlockDoors(1);

    }

    public static event Action<int> OnUnlockDoors;
    public void unlockDoors(int doorType)
    {
        OnUnlockDoors?.Invoke(doorType);
    }

}



///// keeping format to copy here :) ignore pls
///
/*

/// <summary>
/// Holds code for:
/// 
///   ///
///   
/// </summary>



    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///

 */
