using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.InputSystem.UI;

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
    public playerBase player;
    public playerBase playerDT;
    public playerBase playerVR;
    
    [Header("Progress Tracking")]
    public bool entityIsSpawned;
    public bool diningUnlocked;
    public bool[] puzzlesCompleted; // track completion
    public int numPuzzlesCompleted;
    public int currPuz; //current puzzle
    public int potentialNewPuz;

    [Header("UI")]
    public Sprite healthTrue;
    public Sprite healthFalse;

    public Image[] healthDisplay = new Image[3];
    public Canvas menuCanvas;
    public GameObject winPanel;
    public Camera vrCam;
    public Camera desktopCam;
    public GameObject vrPlayer;
    public GameObject desktopPlayer;
    public XRUIInputModule xrModule;
    public InputSystemUIInputModule desktopModule;

    [Header("Objects")]
    public GameObject[] candlesArr;
    public GameObject[] candlePlacements;
    public AudioSource candleSpawnSound;
    public AudioSource entityScream;
    public ParticleSystem entityParticles;

    public static event Action<int> OnUnlockDoors;
    public static event Action OnChaseStarted;

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
        if (playerBase.desktopMode) player = playerDT;
        else { player = playerVR;  }

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
        //check if screaming is doe to load the win panel
        if(entityScream.time >= 6.0f)
        {
            //show winning panel
            sceneManager.gameIsPaused = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            winPanel.SetActive(true);
            menuCanvas.gameObject.SetActive(true);
        }
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
            if (settingsButton.captionsOn)
            {
                dialogueBase.dialogueScript.setDialogue("*Spawning Sound*", 3f);
            }
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
            if (settingsButton.captionsOn)
            {
                dialogueBase.dialogueScript.setDialogue("*Demonic Screams*", 3f);

            }
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

    public void updateCurrPuzz(int newIndex)
    {
        currPuz = newIndex;
        entityBase.entity.initEntityWait();
    }

    public void unlockDoors(int doorType)
    {
        OnUnlockDoors?.Invoke(doorType);
    }

    public void startChase()
    {
        OnChaseStarted?.Invoke();
    }

    public void setVrMode()
    {
        menuCanvas.worldCamera = vrCam;
        menuCanvas.transform.SetParent(vrCam.transform);
        desktopModule.enabled = false;
        xrModule.enabled = true;
    }

    public void setDesktopMode()
    {
        menuCanvas.worldCamera = desktopCam;
        menuCanvas.transform.SetParent(desktopCam.transform);
        desktopModule.enabled = true;
        xrModule.enabled = false;
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
