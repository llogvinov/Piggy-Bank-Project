using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("Settings Events")]
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private Button openSettingsButton;

    [Header("Settings Buttons")]
    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button noAddsButton;
    [Space(20f)]
    [SerializeField] private Image noMusicImage;
    [SerializeField] private Image noSoundImage;

    private void Start()
    {
        AddSettingsEvents();

        RemoveAdsComplete();

        if (PlayerPrefs.GetFloat("music") == 0) { noMusicImage.gameObject.SetActive(true); }
        if (PlayerPrefs.GetFloat("sounds") == 0) { noSoundImage.gameObject.SetActive(true); }
    }

    private void AddSettingsEvents()
    {
        openSettingsButton.onClick.RemoveAllListeners();
        openSettingsButton.onClick.AddListener(OpenCloseSettings);

        musicButton.onClick.RemoveAllListeners();
        musicButton.onClick.AddListener(Music);

        soundButton.onClick.RemoveAllListeners();
        soundButton.onClick.AddListener(Sound);
    }

    //Opens and closes Settings UI
    private void OpenCloseSettings()
    {
        settingsUI.SetActive(!settingsUI.activeSelf);
    }

    public void RemoveAdsComplete()
    {
        if (GameDataManager.IsRemovedAds()) 
        { 
            noAddsButton.gameObject.SetActive(false); 
        }
    }

    //Controls Music UI element
    //Turns on or off the music
    private void Music()
    {
        noMusicImage.gameObject.SetActive(!noMusicImage.gameObject.activeSelf);
        if (noMusicImage.gameObject.activeSelf) 
        { 
            PlayerPrefs.SetFloat("music", 0f); 
        }
        else 
        { 
            PlayerPrefs.SetFloat("music", 1f); 
        }
    }

    //Controls Sounds UI element
    //Turns on or off the sounds
    private void Sound()
    {
        noSoundImage.gameObject.SetActive(!noSoundImage.gameObject.activeSelf);
        if (noSoundImage.gameObject.activeSelf) 
        { 
            PlayerPrefs.SetFloat("sounds", 0f); 
        }
        else 
        { 
            PlayerPrefs.SetFloat("sounds", 1f); 
        }
    }


}
