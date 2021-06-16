using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController
{
    private static int mainScene = 0;

    public static void LoadMainScene()
    {
        LoadScene(mainScene);
    }

    public static void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene < SceneManager.sceneCountInBuildSettings)
        {
            LoadScene(currentScene + 1);
        }        
    }

    public static void LoadPreviousScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene > 0)
        {
            LoadScene(currentScene - 1);
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
