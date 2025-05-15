using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UI;

public class HatItemUI : MonoBehaviour, IItemUI
{
	[SerializeField] private Color itemNotSelectedColor;
	[SerializeField] private Color itemSelectedColor;

	[Space(20f)]
	[SerializeField] private Image hatImage;
	[SerializeField] private LocalizedText localizedText;
	[SerializeField] private Text hatPriceText;
	[SerializeField] private Button hatPurchaseButton;

	[Space(20f)]
	[SerializeField] private Button itemButton;
	[SerializeField] private Image itemImage;

	private Hat _hat;
	public Hat Hat => _hat;

	public void Initialize(Hat hat)
	{
		_hat = hat;
		SetHatName(hat.LocalizationId);
		SetHatPrice(hat.Price);
		if (hat.Image != null)
		{
			SetHatImage(hat.Image);
		}
		else
		{
			SetHatImageOpacity();
		}
	}

    public void SetHatName(string localizationId)
    {
        localizedText.SetId(localizationId);
		localizedText.UpdateText();
    }

    public void SetHatPrice(int price) => 
		hatPriceText.text = price.ToString();

	public void SetHatImage(Sprite sprite) => 
		hatImage.sprite = sprite;

	public void SetHatImageOpacity() => 
		hatImage.color = new Color(0f, 0f, 0f, 0f);

	public void SetItemAsPurchased()
	{
		hatPurchaseButton.gameObject.SetActive(false);
		itemButton.interactable = true;

		itemImage.color = itemNotSelectedColor;
	}

	public void SetItemAsNotPurchased()
	{
		
	}

	public void OnItemPurchase(int itemIndex, UnityAction<int> action)
	{
		hatPurchaseButton.onClick.RemoveAllListeners();
		hatPurchaseButton.onClick.AddListener(() => action?.Invoke(itemIndex));
	}

	public void OnItemSelect(int itemIndex, UnityAction<int> action)
	{
		itemButton.interactable = true;

		itemButton.onClick.RemoveAllListeners();
		itemButton.onClick.AddListener(() => action?.Invoke(itemIndex));
	}

	public void SelectItem()
	{
		itemImage.color = itemSelectedColor;
		itemButton.interactable = false;
	}

	public void DeselectItem()
	{
		itemImage.color = itemNotSelectedColor;
		itemButton.interactable = true;
	}
}
