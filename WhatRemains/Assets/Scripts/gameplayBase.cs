using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Holds code for:
/// 
///   Global variables 
///   set current puzzle to 0 for none
/// 
///   Ending the game
///   
///   Placed this script on candlesFolder
///   
///
///   
/// </summary>

public class gameplayBase : MonoBehaviour
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    public static gameplayBase instance;
    
    [Header("Progress Tracking")]
    public bool diningUnlocked;
    public bool[] puzzlesCompleted; // track completion
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
        if (instance == null) { Debug.Log("no gameplayBase instance, youre cooked buddy :(((( "); }


        puzzlesCompleted = new bool[] { false, false, false, false, false };
        currPuz = 0;
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
        puzzlesCompleted[currPuzInt] = true;

        // set active curr candle :)
        candlesArr[currPuzInt].SetActive(true);
        candlePlacements[currPuzInt].SetActive(true);

        //play the candle spawn sound
        candleSpawnSound.Play();
    }

    public void completeGame()
    {
        //entity screams
        if (!entityScream.isPlaying)
        {
            entityScream.Play();
        }
        entityParticles.Play();
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
