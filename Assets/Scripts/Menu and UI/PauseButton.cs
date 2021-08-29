using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    
    private Button pauseButton;

    private void Awake() => pauseButton = GetComponent<Button>();

    private void Start()
    {
        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(PauseGame);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

}
