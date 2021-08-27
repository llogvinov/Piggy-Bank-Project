using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header("Shop Events")]
    [SerializeField] private GameObject shopUI;
    [SerializeField] private Button openShopButton;
    [SerializeField] private Button closeShopButton;

    [Header("Buttons to hide on panel open")]
    [SerializeField] private Button[] buttons;

    private void Start() => AddShopButtonsEvents();

    private void AddShopButtonsEvents()
    {
        openShopButton.onClick.RemoveAllListeners();
        openShopButton.onClick.AddListener(OpenShop);

        closeShopButton.onClick.RemoveAllListeners();
        closeShopButton.onClick.AddListener(CloseShop);
    }

    private void OpenShop()
    {
        shopUI.SetActive(true);
        
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void CloseShop()
    {
        shopUI.SetActive(false);
        
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

}
