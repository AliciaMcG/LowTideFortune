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
        textObj.text = "";
        diaIsActive = false;
        timeLeft = 0f;

        gameObject.SetActive(false);
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (diaIsActive)        {
            timeLeft -= Time.fixedDeltaTime;

            if (timeLeft <= 0f)            {
                diaIsActive = false;
                textObj.text = "";

                gameObject.SetActive(false);
            }
        }
    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    public void setDialogue(string dialogue, float timeNeeded)
    {
        timeLeft = timeNeeded;
        textObj.text = dialogue;

        diaIsActive = true;
        gameObject.SetActive(true);
    }
}
