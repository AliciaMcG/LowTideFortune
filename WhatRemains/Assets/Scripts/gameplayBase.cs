using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds code for:
/// 
///   Global variables like which rooms unlocked 
///   set current puzzle to 7 for none
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

    [Header("Objects")]
    public GameObject[] candlesArr;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            if (instance == null) { Debug.Log("destroyed wrong gameplayBase instance??"); }
        }
        instance = this;
        if (instance == null) { Debug.Log("no gameplayBase instance, youre cooked buddy :(((( "); }


        puzzlesCompleted = new bool[] { false, false, false, false, false };
    }

    void Start()
    {
        foreach (GameObject candle  in candlesArr) {
            candle.SetActive(false);
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
