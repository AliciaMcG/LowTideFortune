using UnityEngine;

public class Ritual1Behaviour : MonoBehaviour
{
    RitualsManager ritualsManager;
    bool active = true;

    const int totalTime = 3000;
    float timePassed = -1;
    public Camera playerCam;
    public float range = 5f;

    // Update is called once per frame
    void Update()
    {
        if (active && timePassed < totalTime)
        {
            Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, range))
            {
                if (hit.collider.CompareTag("Ritual1Symbol"))
                {
                    // player watching symbol
                    if (timePassed < 0)
                    {
                        // start timer
                        timePassed = 0;
                    }
                    else
                    {
                        // increase time passed
                        timePassed += Time.deltaTime;
                    }
                }
                else if (timePassed > -0.5)
                {
                    //reset timer
                    timePassed = -1;
                }
            }
        }
        else if (active && timePassed >= totalTime)
        {
            // player has watched symbol for necessary time
            completeRitual();
        }
    }

    void completeRitual()
    {
        // door is unlcoekd, entity is activated
        active = false;
        ritualsManager.ritualSetComplete(1);
    }
}
