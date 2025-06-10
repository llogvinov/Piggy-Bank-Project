using System.Collections;
using UnityEngine;
using YG;

namespace UI
{
    public class PurchaseLocalizer : MonoBehaviour
    {
        [SerializeField] private PurchaseYG _purchaseYG;
        [SerializeField] private LocalizedText _titleLocalizedText;
        [SerializeField] private LocalizedText _descriptionLocalizedText;

        private IEnumerator Start()
        {
            yield return null;

            _titleLocalizedText.SetId("in-app" + _purchaseYG.id);
            _titleLocalizedText.UpdateText();

            _descriptionLocalizedText.SetId("in-app" + _purchaseYG.id + "_description");
            _descriptionLocalizedText.UpdateText();
        }
    }
}