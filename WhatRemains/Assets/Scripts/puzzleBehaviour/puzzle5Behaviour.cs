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


    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
    }

    void Start()
    {
        gameplayBase.OnChaseStarted += activatePuzz5;
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

        gameplayBase.instance.candleSpawnSound.Play();
        if (settingsButton.captionsOn)
        {
            dialogueBase.dialogueScript.setDialogue("*Spawning Sound*", 3f);
        }

        entityBase.entity.entityState = 3; //chase state

        //gameplayBase.instance.unlockDoors(3);

    }
}
