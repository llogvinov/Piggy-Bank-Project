using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MaskItemUI : MonoBehaviour, IItemUI
{
	[SerializeField] private Color itemNotSelectedColor;
	[SerializeField] private Color itemSelectedColor;

	[Space(20f)]
	[SerializeField] private Image maskImage;
	[SerializeField] private Text maskNameText;
	[SerializeField] private Text maskPriceText;
	[SerializeField] private Button maskPurchaseButton;

	[Space(20f)]
	[SerializeField] private Button itemButton;
	[SerializeField] private Image itemImage;

	public void SetItemPosition(Vector2 pos) => GetComponent<RectTransform>().anchoredPosition += pos;

	public void SetMaskImage(Sprite sprite) => maskImage.sprite = sprite;

	public void SetMaskImageOpacity() => maskImage.color = new Color(0f, 0f, 0f, 0f);

	public void SetMaskName(string name) => maskNameText.text = name;

	public void SetMaskPrice(int price) => maskPriceText.text = price.ToString();

	public void SetItemAsPurchased()
	{
		maskPurchaseButton.gameObject.SetActive(false);
		itemButton.interactable = true;

		itemImage.color = itemNotSelectedColor;
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
