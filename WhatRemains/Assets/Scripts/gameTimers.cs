using UnityEngine;
using TMPro; 
using System.Collections;

/// <summary>
/// Holds code for:
/// 
///   All timing components, including:
///   
///   Unlocking doors
/// 
///   Opening the final room door
/// 
///   Setting the game timer (clock)
/// 
///   Dissolving safety rooms
///   
/// </summary>


public class gameTimers : MonoBehaviour
{
    public TMP_Text gameTime;
    public static int currentGameMinute;
    public static int currentGameHour;
    float deltaTime = 0;
    int elapsedMinutes = 0;
    public gameplayBase gameplayBaseObj;
    public Animator door;

    public AudioSource unlockSound;
    public AudioSource doorOpenSound;
    bool doorOpenedPlayed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentGameMinute = 0;
        currentGameHour = 12;
        doorOpenedPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if the game's not paused
        if (sceneManager.gameIsPaused == false)
        {
            //setting the clock
            //get the current second of the game
            deltaTime += Time.deltaTime;
            elapsedMinutes = (int)deltaTime;

            //after the first 60 seconds, set the hour back to 1
            if (elapsedMinutes < 60)
            {
                currentGameHour = 12;
            }
            else
            {
                //the hour is elapsed minutes divided by 60
                currentGameHour = elapsedMinutes / 60;
            }

            //the minute is the remainder of the seconds divide by 60 
            currentGameMinute = (int)deltaTime % 60; 

            //if the minute is less than 10, add a 0 in front of it
            if (currentGameMinute < 10)
            {
                gameTime.text = currentGameHour + ":0" + (int)currentGameMinute + "am";
            }
            else
            {
                gameTime.text = currentGameHour + ":" + (int)currentGameMinute + "am";
            }

            //unlock the dining room door at 4 minutes
            bool diningDoorUnlocked = false;
            if (currentGameHour == 4 && currentGameMinute == 0 && diningDoorUnlocked == false)
            {
                gameplayBaseObj.unlockDoors(2);
                diningDoorUnlocked = true;
                unlockSound.Play();
                if (settingsButton.captionsOn)
                {
                    dialogueBase.dialogueScript.setDialogue("*Door Unlocking*", 3f);
                }
            }

            //open the final room door when all candles have been placed and the player is in the final room
            if (gameplayBaseObj.numPuzzlesCompleted >= 4 && finalRoomTrigger.playerEnteredFinalSR == true)
            {
                gameplayBaseObj.unlockDoors(3);
                door.SetTrigger("Open");
                if (doorOpenedPlayed == false)
                {
                    doorOpenSound.Play();
                    if (settingsButton.captionsOn)
                    {
                        dialogueBase.dialogueScript.setDialogue("*Door Opening*", 3f);
                    }
                    doorOpenedPlayed = true;
                }
            }

            //dissolve safety rooms
            if (gameplayBaseObj.numPuzzlesCompleted == 2)
            {
                //dissolve room 1
            }
            if (gameplayBaseObj.numPuzzlesCompleted == 3)
            {
                //dissolve room 2
            }
            if (gameplayBaseObj.numPuzzlesCompleted == 4)
            {
                //dissolve room 3
            }

        }
    }
}
