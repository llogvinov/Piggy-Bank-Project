using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RefreshLayoutContent : MonoBehaviour
    {
        [SerializeField] private ContentSizeFitter _contentSizeFitter;

        private void OnEnable()
        {
            StartCoroutine(RefreshCoroutine());
        }

        IEnumerator RefreshCoroutine()
        {
            _contentSizeFitter.enabled = false;
            yield return null;
            _contentSizeFitter.enabled = true;
        }
    }
}