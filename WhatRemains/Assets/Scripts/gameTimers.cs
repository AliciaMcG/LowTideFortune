using UnityEngine;
using TMPro; 
using System.Collections;


public class gameTimers : MonoBehaviour
{
    public TMP_Text gameTime;
    public static int currentGameMinute;
    public static int currentGameHour;
    float deltaTime = 0;
    int elapsedMinutes = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentGameMinute = 0;
        currentGameHour = 12;
    }

    // Update is called once per frame
    void Update()
    {
        //seconds represent minutes, minutes represent hours

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
        Debug.Log("Minutes: " + currentGameMinute);
    }
}
