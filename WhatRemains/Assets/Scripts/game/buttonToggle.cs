using UnityEngine;

public class buttonToggle : MonoBehaviour
{
    bool on = false;
    //toggles an object on and off
    public void Toggle()
    {
        if (on)
        {
            gameObject.SetActive(false);
            on = false;
        }
        else
        {
            gameObject.SetActive(true);
            on = true;
        }

    }
}