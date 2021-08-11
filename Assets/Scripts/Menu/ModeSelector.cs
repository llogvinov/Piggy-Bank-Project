using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeSelector : MonoBehaviour
{
    [SerializeField] private GameObject modeUI;
    [SerializeField] private Button openModeButton;
    [SerializeField] private Button closeModeButton;
    [Space]
    [SerializeField] private Button[] modes;
    [Space]
    [SerializeField] private Button[] buttons;

    private void Start()
    {
        AddModeEvents();
    }

    private void AddModeEvents()
    {
        openModeButton.onClick.RemoveAllListeners();
        openModeButton.onClick.AddListener(OpenMode);

        closeModeButton.onClick.RemoveAllListeners();
        closeModeButton.onClick.AddListener(CloseMode);
    }

    private void OpenMode()
    {
        modeUI.SetActive(true);

        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }

    }

    private void CloseMode()
    {
        modeUI.SetActive(false);

        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

    public static void LoadScene(int index)
    {
        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(index);
        }
    }

}
