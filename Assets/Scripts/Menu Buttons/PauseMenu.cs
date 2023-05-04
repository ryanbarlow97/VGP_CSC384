using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public bool IsGamePaused { get; private set; } = false;


    private void Start()
    {
        pauseMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    
    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }
}
