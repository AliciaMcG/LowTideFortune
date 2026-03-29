using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class settingsButton : MonoBehaviour
{
    //mouse sensitivty
    public Slider mouseSensitivitySlider;
    public float mouseSensitivity = 1f;

    //text size
    public TMP_Dropdown textSizeOptions;

    public string textSize = "Standard";

    public GameObject settingsMenu;
    void Start()
    {
        //mouse
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        mouseSensitivitySlider.value = mouseSensitivity;

        //text
        textSize = PlayerPrefs.GetString("TextSize", "Standard");

        if (textSize == "Standard")
        {
            textSizeOptions.value = 0;
        }
        else if (textSize == "Small")
        {
            textSizeOptions.value = 1;
        }
        else if (textSize == "Large")
        {
            textSizeOptions.value = 2;
        }
        
    }

    //stores any setting changes
    public void SaveSettings()
    {
        //mouse
        mouseSensitivity = mouseSensitivitySlider.value;
        PlayerPrefs.SetFloat("MouseSensitivty", mouseSensitivity);

        //text
        int selectedSizeIdx = textSizeOptions.value;
        switch (selectedSizeIdx)
        {
            case 0:
                textSize = "Standard";
                break;
            case 1:
                textSize = "Small";
                break;
            case 2:
                textSize = "Large";
                break;
        }
        PlayerPrefs.SetString("TextSize", textSize);

        PlayerPrefs.Save();

        settingsMenu.SetActive(false);
    }
}
