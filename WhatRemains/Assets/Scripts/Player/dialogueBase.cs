using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Holds code for:
/// 
///   a function to trigger dialogue from other scripts
///   
/// </summary>

public class dialogueBase : MonoBehaviour
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    public static dialogueBase dialogueScript;

    [Header("Objects")]
    public TextMeshProUGUI textObj;
    public Image panelBackground;

    [Header("Font Size")]
    public float standardSize = 50f;
    public float smallSize = 20;
    public float largeSize = 100f;

    [Header("Vars")]
    private bool diaIsActive;
    private float timeLeft;

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
        if (dialogueScript != null && dialogueScript != this)
        {
            Destroy(gameObject);
        }
        dialogueScript = this;

    }

    void Start()
    {
        ApplyTextSettings();
        HideDialogue();
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (diaIsActive)        {
            timeLeft -= Time.fixedDeltaTime;

            if (timeLeft <= 0f)            {
                HideDialogue();
            }
        }
    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    public void setDialogue(string dialogue, float timeNeeded)
    {
        ApplyTextSettings();

        timeLeft = timeNeeded;
        textObj.text = dialogue;
        diaIsActive = true;

        textObj.enabled = true;
        if(panelBackground != null)
        {
            panelBackground.enabled = true;
        }

        gameObject.SetActive(true);
    }

    private void HideDialogue()
    {
        diaIsActive = false;
        textObj.text = "";
        textObj.enabled = false;
        if(panelBackground != null)
        {
            panelBackground.enabled = false;
        }
    }

    private void ApplyTextSettings()
    {
        if (textObj == null)
        {
            return;
        }

        string sizeSetting = PlayerPrefs.GetString("TextSize", "Standard");
        Debug.Log("Current saved text setting: " + sizeSetting);

        if (sizeSetting == "Large")
        {
            textObj.fontSize = largeSize;
        }
        else if (sizeSetting == "Small")
        {
            textObj.fontSize = smallSize;
        }
        else
        {
            textObj.fontSize = standardSize;
        }
        textObj.SetAllDirty();
    }
}
