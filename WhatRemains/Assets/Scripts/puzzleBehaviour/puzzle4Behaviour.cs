using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Holds code for:
/// 
///   Puzzle index 3
///   
///   skull list order:         
///   
/// </summary>

public class puzzle4Behaviour : MonoBehaviour
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    [Header("Objects")]
    public GameObject[] skullsArr;
    public GameObject[] podiumArr;

    [Header("Lists")]
    public int[] corrOrder;
    public int[] currOrder; 

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
  
    void Start()
    {

    } 
    void Update()
    {
        //if (active)
        //{
        //    if (correctSkullOrder())
        //    {
        //        puzzleManager.completePuzzle(3);
        //    }
        //}
    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    private void check4correctOrder()
    {
        if (currOrder == corrOrder)
        {
            gameplayBase.instance.completePuzzle(3);
            Debug.Log("4 is done for");
        }
    }

    //bool correctSkullOrder()    {
    //    // Check that skulls are near the correct podiums
    //    bool rightRatPos = Math.Abs(ratSkull.transform.position.x - ratPodium.transform.position.x) < 2
    //        && Math.Abs(ratSkull.transform.position.z - ratPodium.transform.position.z) < 2;
    //    bool rightDeerPos = Math.Abs(deerSkull.transform.position.x - deerPodium.transform.position.x) < 2
    //        && Math.Abs(deerSkull.transform.position.z - deerPodium.transform.position.z) < 2;
    //    bool rightOwlPos = Math.Abs(owlSkull.transform.position.x - owlPodium.transform.position.x) < 2
    //        && Math.Abs(owlSkull.transform.position.z - owlPodium.transform.position.z) < 2;

    //    if (rightRatPos && rightDeerPos && rightOwlPos)
    //    {
    //        return true;
    //    }

    //    return false;
    //}
}
