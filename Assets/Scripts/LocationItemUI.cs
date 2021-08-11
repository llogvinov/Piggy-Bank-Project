using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LocationItemUI : MonoBehaviour, IItemUI
{
	[Space(20f)]
	[SerializeField] private Image skyImage;
	[SerializeField] private Image groundImage;
	[SerializeField] private Image treesImage;
	[SerializeField] private Image mountainImage;

	[Space(20f)]
	[SerializeField] private Text locationNameText;
	[SerializeField] private Text locationPriceText;
	[SerializeField] private Button locationPurchaseButton;

	[Space(20f)]
	[SerializeField] private Button chooseLocationButton;

	//--------------------------------------------------------------
	public void SetItemPosition(Vector2 pos)
	{
		GetComponent<RectTransform>().anchoredPosition += pos;
	}

	public void SetLocationImages(Sprite sky, Sprite ground, Sprite trees, Sprite mountain)
	{
		skyImage.sprite = sky;
		groundImage.sprite = ground;
		treesImage.sprite = trees;
		mountainImage.sprite = mountain;
	}

	public void SetLocationName(string name)
	{
		locationNameText.text = name;
	}

	public void SetLocationPrice(int price)
	{
		locationPriceText.text = price.ToString();
	}

	public void SetItemAsPurchased()
	{
		locationPurchaseButton.gameObject.SetActive(false);
		chooseLocationButton.interactable = true;
	}

	public void OnItemPurchase(int itemIndex, UnityAction<int> action)
	{
		locationPurchaseButton.onClick.RemoveAllListeners();
		locationPurchaseButton.onClick.AddListener(() => action.Invoke(itemIndex));
	}

	public void OnItemSelect(int itemIndex, UnityAction<int> action)
	{
		chooseLocationButton.interactable = true;

		chooseLocationButton.onClick.RemoveAllListeners();
		chooseLocationButton.onClick.AddListener(() => action.Invoke(itemIndex));
	}

	public void SelectItem()
	{
		chooseLocationButton.interactable = false;
	}

	public void DeselectItem()
	{
		chooseLocationButton.interactable = true;
	}
}
