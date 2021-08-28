using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HatItemUI : MonoBehaviour, IItemUI
{
	[SerializeField] private Color itemNotSelectedColor;
	[SerializeField] private Color itemSelectedColor;

	[Space(20f)]
	[SerializeField] private Image hatImage;
	[SerializeField] private Text hatNameText;
	[SerializeField] private Text hatPriceText;
	[SerializeField] private Button hatPurchaseButton;

	[Space(20f)]
	[SerializeField] private Button itemButton;
	[SerializeField] private Image itemImage;

	public void SetItemPosition(Vector2 pos) => GetComponent<RectTransform>().anchoredPosition += pos;

	public void SetHatImage(Sprite sprite) => hatImage.sprite = sprite;

	public void SetHatImageOpacity() => hatImage.color = new Color(0f, 0f, 0f, 0f);

	public void SetHatName(string name) => hatNameText.text = name;

	public void SetHatPrice(int price) => hatPriceText.text = price.ToString();

	public void SetItemAsPurchased()
	{
		hatPurchaseButton.gameObject.SetActive(false);
		itemButton.interactable = true;

		itemImage.color = itemNotSelectedColor;
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
