using UnityEngine;

public class chairPull : MonoBehaviour, IPullable
{
    public playerBase playerBaseScript;
    public AudioSource pickupSound;

    //pulls the chair out
    public void pull(playerBase player)
    {
        //play picking up sound
        if (!pickupSound.isPlaying)
        {
            pickupSound.Play();
        }

        //get the chair object
        Transform chair;
        if (playerBase.desktopMode)
        {
            chair = playerBaseScript.hit.transform;
        }
        else
        {
            chair = ;
        }
            //Debug.Log(chair);

            //move the chair back 2 spaces
            chair.Translate(Vector3.right * 2.0f, Space.Self);

        //disable the collider
        chair.GetComponent<BoxCollider>().enabled = false;

        //unparent the tarot card to pick up
        Transform tarotCard = chair.Find("sagittariusTarotCard");
        if (tarotCard != null)
        {
            tarotCard.SetParent(null);
        }
    
    }

}
