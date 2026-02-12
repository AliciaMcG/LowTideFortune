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
    public static bool gameIsPaused;
    public GameObject pauseMenuUI;
    //public static bool gameOver; //FIX (is necessary?? Set up ui??)
    //public GameObject gameOverUI; //
    //public int lastCheckpoint; //FIX (add chckpoints)

    ///////////////////////////////////////////////////////////      LOOPSS      ////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused) { resume(); }
            else { pause(); }
        }

    }


    ///////////////////////////////////////////////////////////      FUNCTIONS      ////////////////////////////////////////////////////////////////////////////////
    ///
    ///////// MAIN MENU ////////////
    public void loadGame()
    {
        SceneManager.LoadSceneAsync("gameplayScene");
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
        Time.timeScale = 0.0f;
        gameIsPaused = true;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
