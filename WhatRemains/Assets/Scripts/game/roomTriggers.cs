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

    public static event Action OnCurrRoomChange;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    public void OnTriggerEnter(Collider other)
    {
        if (roomNum != gameplayBase.instance.player.currRoom)

        
        if (other.GetComponent<playerBase>() != null) { 
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
                        gameplayBase.instance.player.currRoom = 0;
                    }
                    else if (!gameplayBase.instance.puzzlesCompleted[roomNum - 1])
                    {
                        gameplayBase.instance.player.currRoom = roomNum;
                    }
                    else
                    {
                        gameplayBase.instance.player.currRoom = 0;
                    }
                }
                OnCurrRoomChange?.Invoke();
            }
        }

        //Debug.Log("entered room:" +  roomNum);
        Debug.Log("current room: " + gameplayBase.instance.player.currRoom);

    }




}
