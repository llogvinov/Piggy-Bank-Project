using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UI;

public class MaskItemUI : MonoBehaviour, IItemUI
{
	[SerializeField] private Color itemNotSelectedColor;
	[SerializeField] private Color itemSelectedColor;

	[Space(20f)]
	[SerializeField] private Image maskImage;
	[SerializeField] private LocalizedText localizedText;
	[SerializeField] private Text maskPriceText;
	[SerializeField] private Button maskPurchaseButton;

	[Space(20f)]
	[SerializeField] private Button itemButton;
	[SerializeField] private Image itemImage;
	
	private Mask _mask;
	public Mask Mask => _mask;

	public void Initialize(Mask mask)
	{
		_mask = mask;
		SetMaskName(mask.LocalizationId);
		SetMaskPrice(mask.Price);
		if (mask.Image != null)
		{
			SetMaskImage(mask.Image);
		}
		else
		{
			SetMaskImageOpacity();
		}
	}

	public void SetMaskName(string name)
    {
        localizedText.SetId(name);
		localizedText.UpdateText();
    }

	public void SetMaskPrice(int price) => 
		maskPriceText.text = price.ToString();

	public void SetMaskImage(Sprite sprite) => 
		maskImage.sprite = sprite;

	public void SetMaskImageOpacity() => 
		maskImage.color = new Color(0f, 0f, 0f, 0f);

	public void SetItemAsPurchased()
	{
		maskPurchaseButton.gameObject.SetActive(false);
		itemButton.interactable = true;

		itemImage.color = itemNotSelectedColor;
	}

	public void SetItemAsNotPurchased()
	{
		
	}

	public void OnItemPurchase(int itemIndex, UnityAction<int> action)
	{
		maskPurchaseButton.onClick.RemoveAllListeners();
		maskPurchaseButton.onClick.AddListener(() => action?.Invoke(itemIndex));
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
