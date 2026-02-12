using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds code for:
/// 
///   Global variables like which rooms unlocked 
///   set current puzzle to 0 for none
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
    public GameObject player;
    public List<GameObject> candlesList;   //   !!!!!!! ----- CANDLES INDEXES ARE SHIFTED DOWN BY ONE IN LIST ----- !!!!!!

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
        puzzlesCompleted = new List<bool> { false, false, false, false, false};
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
    public void completeRitual(int currPuzInt)    // skulls are in the correct placement
    {
        //active  = false
        puzzlesCompleted[currPuzInt] = true;
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
