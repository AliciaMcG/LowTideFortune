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
    [Header("Progress Tracking")]
    public bool diningUnlocked;
    public List<bool> puzzlesCompleted; // track completion
    public int currPuz; //current puzzle

    [Header("Objects")]
    public List<GameObject> candlesList;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
        puzzlesCompleted = new List<bool> { false, false, false, false, false};
    }

    void Start()
    {
        foreach (GameObject candle  in candlesList) {
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
        candlesList[currPuzInt].SetActive(true);
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
