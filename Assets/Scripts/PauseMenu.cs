using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    public static bool isGamePaused = false;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        pausePanel.SetActive(false);
    }

}
