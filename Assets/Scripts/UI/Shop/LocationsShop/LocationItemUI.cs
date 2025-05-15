using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UI;

public class LocationItemUI : MonoBehaviour, IItemUI
{
	[Space(20f)]
	[SerializeField] private Image skyImage;
	[SerializeField] private Image groundImage;
	[SerializeField] private Image treesImage;
	[SerializeField] private Image mountainImage;

	[Space(20f)]
	[SerializeField] private LocalizedText localizedText;
	[SerializeField] private Text locationPriceText;
	[SerializeField] private Button locationPurchaseButton;

	[Space(20f)]
	[SerializeField] private Button chooseLocationButton;

	private Location _location;
	public Location Location => _location;

	public void Initialize(Location location)
	{
		_location = location;
		SetLocationName(location.LocalizationId);
		SetLocationPrice(location.Price);
		SetLocationImages(location);
	}

	public void SetLocationName(string name)
	{
        localizedText.SetId(name);
		localizedText.UpdateText();
    }

	public void SetLocationPrice(int price) => 
		locationPriceText.text = price.ToString();

	public void SetLocationImages(Location location)
	{
		skyImage.sprite = location.sky;
		groundImage.sprite = location.ground;
		treesImage.sprite = location.trees;
		mountainImage.sprite = location.mountain;
	}

	public void SetItemAsPurchased()
	{
		locationPurchaseButton.gameObject.SetActive(false);
		chooseLocationButton.interactable = true;
	}

	public void SetItemAsNotPurchased()
	{
		
	}

	public void OnItemPurchase(int itemIndex, UnityAction<int> action)
	{
		locationPurchaseButton.onClick.RemoveAllListeners();
		locationPurchaseButton.onClick.AddListener(() => action?.Invoke(itemIndex));
	}

	public void OnItemSelect(int itemIndex, UnityAction<int> action)
	{
		chooseLocationButton.interactable = true;

		chooseLocationButton.onClick.RemoveAllListeners();
		chooseLocationButton.onClick.AddListener(() => action?.Invoke(itemIndex));
	}

	public void SelectItem() => 
		chooseLocationButton.interactable = false;

	public void DeselectItem() => 
		chooseLocationButton.interactable = true;
}
