using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System;

/// <summary>
/// Holds code for:
/// 
///   room number beteen ONE and FIVE
///   
///   0 is none (general)
///   6 is dining
///   7 is safe room
///   
/// </summary>

public class roomTriggers : MonoBehaviour
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    public int roomNum;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    public void OnTriggerEnter(Collider other)
    {
        if (roomNum >= 0 && roomNum <= 5)
        {

            if (roomNum == 1)
            {
                // nothing if room 1 entered 
            }
            else
            {
                // BTW 0 is for no active puzzle :)
                if (roomNum == 0)
                {
                    gameplayBase.instance.currPuz = 0;
                }
                else if (!gameplayBase.instance.puzzlesCompleted[roomNum - 1])
                {
                    gameplayBase.instance.currPuz = roomNum;
                }
                else
                {
                    gameplayBase.instance.currPuz = 0;
                }

            }
            cpChangeUpdate();
        }

        //Debug.Log("entered room:" +  roomNum);
        Debug.Log("current puzzle: " + gameplayBase.instance.currPuz);

        //if (other.TryGetComponent<playerBase>(out playerBase player)) {
        //    if (0 <= roomNum && roomNum >= 7)
        //    {
        //        player.currRoom = roomNum;

        //        if (2 <= roomNum && roomNum >= 5)
        //        {
        //            gameplayBase.instance.potentialNewPuz = roomNum; //FIX
        //        }           


        //} }

    }


    public static event Action OnCurrPuzzChange;
    public void cpChangeUpdate ()
    {
        OnCurrPuzzChange?.Invoke();
    }

}
