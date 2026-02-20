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
    public bool entityIsActive;

    [Header("Objects")]
    public List<GameObject> candlesList;
    public GameObject entityObject;

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

        entityIsActive = false;
    }

    void Update()
    {
        checkEntityActivity();

    }

    private void FixedUpdate()
    {
    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    public void completePuzzle(int currPuzInd)    // skulls are in the correct placement
    {
        puzzlesCompleted[currPuzInd] = true;

        // set active curr candle :)
        candlesList[currPuzInd].SetActive(true);
    }
    void checkEntityActivity()
    {
        if (entityIsActive) {
            entityObject.SetActive(true);
        }
        else { entityObject.SetActive(false); }
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
