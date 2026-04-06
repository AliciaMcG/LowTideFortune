using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Holds code for:
/// 
///   Travelling between scenes, 
///   Pause screen
///   
/// </summary>

public class sceneManager : MonoBehaviour
{
    ///////////////////////////////////////////////////////////      VARS      ////////////////////////////////////////////////////////////////////////////////
    public static sceneManager instance; //not multiplayer?? fix????

    public static bool gameIsPaused;
    public GameObject pauseMenuUI;
    public GameObject mainMenuPanel;
    public GameObject instructionsPanel;
    public GameObject gamemodePanel;
    public static bool gameOver; //FIX (is necessary?? Set up ui??)
    //public int lastCheckpoint; //FIX (add chckpoints)


    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "gameplayScene") { 
            pauseMenuUI.SetActive(false);
        }
        //Time.timeScale = 1.0f;
        gameIsPaused = false;

        gameOver = false;


        
    }


    void Update()
    {
        if (SceneManager.GetActiveScene().name == "gameplayScene") {
            if (Input.GetKeyDown(KeyCode.BackQuote)) {
                if (gameIsPaused) { resume(); }
                else { pause(); }
            }
        }

    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    ///////// MAIN MENU ////////////
    public void loadGame()
    {
        SceneManager.LoadSceneAsync("gameplayScene");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void quitGame()
    {
        Debug.Log("Player quit game :(");
        Application.Quit();
    }

    ///////// GAMEPLAY ////////////
    public void return2main()
    {
        //fix (do save?? or last checkpoint??)
        SceneManager.LoadSceneAsync("mainMenuScene");
    }

    ///////// PAUSE AND GAME OVER ////////////
    public void pause()
    {
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0.0f;
        gameIsPaused = true;
        //FIX - PAUSE ENTITY MOVEMENT

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void resume()
    {
        pauseMenuUI.SetActive(false);
        //Time.timeScale = 1.0f;
        gameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void instructions()
    {
        mainMenuPanel.SetActive(false);
        instructionsPanel.SetActive(true);

    }

    public void gamemodeSelect()
    {
        mainMenuPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        gamemodePanel.SetActive(true);

    }

    public void setVrMode()
    {
        playerBase.desktopMode = false;
        loadGame();
    }
    public void setDesktopMode()
    {
        playerBase.desktopMode = true;
        loadGame();
    }

}
