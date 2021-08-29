using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private Button menuButton;
    private int mainScene = 0;

    private void Awake() => menuButton = GetComponent<Button>();

    private void Start()
    {
        menuButton.onClick.RemoveAllListeners();
        menuButton.onClick.AddListener(LoadMainScene);
    }

    private void LoadMainScene()
    {
        LoadScene(mainScene);
        
        Time.timeScale = 1f;
    }
    
    private static void LoadScene(int index)
    {
        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(index);
    }

}
