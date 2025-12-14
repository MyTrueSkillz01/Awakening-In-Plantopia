using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;
    
    private bool isPaused = false;

    private void Start()
    {
        // Make sure menus are hidden at start
        if(pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
        
        if(gameOverMenuUI != null)
            gameOverMenuUI.SetActive(false);
    }

    private void Update()
    {
        // Press ESC to toggle pause menu
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        if(pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f; // Freeze the game
            isPaused = true;
        }
    }

    public void Resume()
    {
        if(pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f; // Resume the game
            isPaused = false;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Make sure time is running
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Make sure time is running
        SceneManager.LoadScene("MainMenu"); // Change this to your main menu scene name
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void ShowGameOver()
    {
        if(gameOverMenuUI != null)
        {
            gameOverMenuUI.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
    }
}