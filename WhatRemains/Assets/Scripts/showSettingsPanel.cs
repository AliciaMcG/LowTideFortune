using UnityEngine;

public class showSettingsPanel : MonoBehaviour
{
    public GameObject settingsPanel;

    public void TogglePanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
