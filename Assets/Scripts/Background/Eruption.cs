using System.Collections;
using UnityEngine;

namespace Main.Background
{
    public class Eruption : MonoBehaviour
    {
        [SerializeField] private Animator _eruption;

        [SerializeField] private float minTimeEruption = 5;
        [SerializeField] private float maxTimeEruption = 10;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minTimeEruption, maxTimeEruption));
                PlayEruptionAnimation();
            }
        }

        private void PlayEruptionAnimation()
        {
            _eruption.gameObject.SetActive(true);
            _eruption.Play("LavaAnimation");
        }
    }
}