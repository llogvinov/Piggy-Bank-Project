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
        AddSettingsButtonsEvents();

        RemoveAdsComplete();

        if (PlayerPrefs.GetFloat("music") == 0) 
            noMusicImage.gameObject.SetActive(true);
        if (PlayerPrefs.GetFloat("sounds") == 0) 
            noSoundImage.gameObject.SetActive(true);
    }

    private void AddSettingsButtonsEvents()
    {
        openSettingsButton.onClick.RemoveAllListeners();
        openSettingsButton.onClick.AddListener(OpenCloseSettings);

        musicButton.onClick.RemoveAllListeners();
        musicButton.onClick.AddListener(Music);

        soundButton.onClick.RemoveAllListeners();
        soundButton.onClick.AddListener(Sound);
    }

    //Opens and closes Settings UI
    private void OpenCloseSettings() => settingsUI.SetActive(!settingsUI.activeSelf);

    public void RemoveAdsComplete()
    {
        if (GameDataManager.IsRemovedAds()) 
            noAddsButton.gameObject.SetActive(false); 
    }

    //Controls Music UI element
    //Turns on or off the music
    private void Music()
    {
        var noMusic = noMusicImage.gameObject;
        noMusic.SetActive(!noMusic.activeSelf);
        
        PlayerPrefs.SetFloat("music", noMusic.activeSelf ? 0f : 1f);
    }

    //Controls Sounds UI element
    //Turns on or off the sounds
    private void Sound()
    {
        var noSound = noSoundImage.gameObject;
        noSound.SetActive(!noSound.activeSelf);
        
        PlayerPrefs.SetFloat("sounds", noSound.activeSelf ? 0f : 1f);
    }


}
