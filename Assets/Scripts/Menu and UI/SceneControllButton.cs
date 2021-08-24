using UnityEngine;
using UnityEngine.UI;

public class SceneControllButton : MonoBehaviour
{
    enum TargetScene
    {
        Next,
        Previous,
        MainMenu
    }

    [SerializeField] private TargetScene targetScene;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        switch (targetScene)
        {
            case TargetScene.MainMenu:
                button.onClick.AddListener(() => SceneController.LoadMainScene());
                break;

            case TargetScene.Next:
                button.onClick.AddListener(() => SceneController.LoadNextScene());
                break;

            case TargetScene.Previous:
                button.onClick.AddListener(() => SceneController.LoadPreviousScene());
                break;
        }
    }

}
