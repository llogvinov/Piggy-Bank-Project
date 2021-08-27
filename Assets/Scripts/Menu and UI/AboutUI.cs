using UnityEngine;
using UnityEngine.UI;

public class AboutUI : MonoBehaviour
{
    [Header("About Events")]
    [SerializeField] private GameObject aboutUI;
    [SerializeField] private Button openAboutButton;
    [SerializeField] private Button closeAboutButton;

    [Header("About Buttons")]
    [SerializeField] private Button moreButton;
    [SerializeField] private Button rateButton;
    [SerializeField] private Button feedback;
    [SerializeField] private Button privacyPolicyButton;
    [SerializeField] private Button termsButton;

    [Header("Buttons to hide on panel open")]
    [SerializeField] private Button[] buttons;

    private const string PrivacyPolicy = "https://piggybank.flycricket.io/privacy.html";
    private const string Terms = "https://piggybank.flycricket.io/terms.html";
    private const string More = "https://play.google.com/store/apps/developer?id=Cringe+Games+LS";
    private const string Rate = "https://play.google.com/store/apps/details?id=com.CringeGamesLS.PiggyBank";
    private const string MailAddress = "cringeGamesOfficial@gmail.com";
    
    private void Start() => AddAboutButtonsEvents();

    private void AddAboutButtonsEvents()
    {
        openAboutButton.onClick.RemoveAllListeners();
        openAboutButton.onClick.AddListener(OpenAbout);

        closeAboutButton.onClick.RemoveAllListeners();
        closeAboutButton.onClick.AddListener(CloseAbout);

        moreButton.onClick.RemoveAllListeners();
        moreButton.onClick.AddListener(OpenMore);
        
        rateButton.onClick.RemoveAllListeners();
        rateButton.onClick.AddListener(OpenRate);
        
        feedback.onClick.RemoveAllListeners();
        feedback.onClick.AddListener(Feedback);
        
        privacyPolicyButton.onClick.RemoveAllListeners();
        privacyPolicyButton.onClick.AddListener(OpenPrivacyPolicy);

        termsButton.onClick.RemoveAllListeners();
        termsButton.onClick.AddListener(OpenTerms);
    }

    private void OpenAbout()
    {
        aboutUI.SetActive(true);

        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void CloseAbout()
    {
        aboutUI.SetActive(false);

        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

    private void OpenPrivacyPolicy() => Application.OpenURL(PrivacyPolicy);

    private void OpenTerms() => Application.OpenURL(Terms);
    
    private void OpenMore() => Application.OpenURL(More);

    private void OpenRate() => Application.OpenURL(Rate);

    private void Feedback() => Application.OpenURL("mailto:" + MailAddress);
    
}
