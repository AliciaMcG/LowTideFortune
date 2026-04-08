using System;
using UnityEngine;

/// <summary>
/// Holds code for:
/// 
///   ///
///   
/// </summary>

public class puzzle5Behaviour : MonoBehaviour
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    public gameTimers gameTimerScript;

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

    public void activatePuzz5()
    {
        gameplayBase.instance.candlesArr[4].SetActive(true);
        gameplayBase.instance.candlePlacements[4].SetActive(true);
        
        gameTimerScript.unlockSound.Play();
        if (settingsButton.captionsOn)
        {
            dialogueBase.dialogueScript.setDialogue("*Door Unlocks*", 3f);
        }
        

        gameplayBase.instance.unlockDoors(3);

    }
}
