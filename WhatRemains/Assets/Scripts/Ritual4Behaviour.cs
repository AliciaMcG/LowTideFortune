using UnityEngine;
using System;

public class Ritual4Behaviour : MonoBehaviour
{
    RitualsManager ritualsManager;
    bool active = true;

    public GameObject ratSkull;
    public GameObject deerSkull;
    public GameObject owlSkull;
    public GameObject ratPodium;
    public GameObject deerPodium;
    public GameObject owlPodium;

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (checkSkullOrder())
            {
                completeRitual();
            }
        }
    }

    bool checkSkullOrder()
    {
        // Check that skulls are near the correct podiums
        bool rightRatPos = Math.Abs(ratSkull.transform.position.x - ratPodium.transform.position.x) < 2
            && Math.Abs(ratSkull.transform.position.z - ratPodium.transform.position.z) < 2;
        bool rightDeerPos = Math.Abs(deerSkull.transform.position.x - deerPodium.transform.position.x) < 2
            && Math.Abs(deerSkull.transform.position.z - deerPodium.transform.position.z) < 2;
        bool rightOwlPos = Math.Abs(owlSkull.transform.position.x - owlPodium.transform.position.x) < 2
            && Math.Abs(owlSkull.transform.position.z - owlPodium.transform.position.z) < 2;

        if (rightRatPos && rightDeerPos && rightOwlPos)
        {
            return true;
        }

        return false;
    }

    void completeRitual()
    {
        // skulls are in the correct placement
        active = false;
        ritualsManager.ritualSetComplete(4);
    }
}
