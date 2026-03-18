using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System;

/// <summary>
/// Holds code for:
/// 
///   room number beteen ONE and FIVE
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
        if (roomNum >= 0 && roomNum <= 5)        {

            if (roomNum == 1)
            {
                // nothing if room 1 entered 
            }
            else
            {
                // BTW 0 is for no active puzzle :)
                if(roomNum == 0)
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

    }


    public static event Action OnCurrPuzzChange;
    public void cpChangeUpdate ()
    {
        OnCurrPuzzChange?.Invoke();
    }

}
