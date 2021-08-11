using UnityEngine;
using UnityEngine.UI;

public class AboutUI : MonoBehaviour
{
    [Header("About Events")]
    [SerializeField] private GameObject aboutUI;
    [SerializeField] private Button openAboutButton;
    [SerializeField] private Button closeAboutButton;

    [Header("About Buttons")]
    //[SerializeField] private Button moreButton;
    //[SerializeField] private Button rateButton;
    [SerializeField] private Button privacyPolicyButton;
    [SerializeField] private Button termsButton;

    [SerializeField] private Button[] buttons;

    private const string privacyPolicy = "https://piggybank.flycricket.io/privacy.html";
    private const string terms = "https://piggybank.flycricket.io/terms.html";
    //private const string more;
    //private const string rate;

    void Start()
    {
        AddAboutEvents();
    }

    private void AddAboutEvents()
    {
        openAboutButton.onClick.RemoveAllListeners();
        openAboutButton.onClick.AddListener(OpenAbout);

        closeAboutButton.onClick.RemoveAllListeners();
        closeAboutButton.onClick.AddListener(CloseAbout);

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

    private void OpenPrivacyPolicy()
    {
        Application.OpenURL(privacyPolicy);
    }

    private void OpenTerms()
    {
        Application.OpenURL(terms);
    }

    /*
    private void OpenMore()
    {
        Application.OpenURL(more);
    }

    private void OpenRate()
    {
        Application.OpenURL(rate);
    }
    */
}
