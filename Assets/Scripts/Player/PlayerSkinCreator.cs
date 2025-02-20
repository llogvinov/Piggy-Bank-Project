using UnityEngine;

namespace Main.Player
{
    public class PlayerSkinCreator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _hat;
        [SerializeField] private SpriteRenderer _mask;

        private void Start()
        {
            SetFullSkin();
        }

        public void SetFullSkin()
        {
            SetMask();
            SetHat();
        }

        public void SetMask()
        {
            var mask = GameDataManager.GetSelectedMask();
            _mask.sprite = mask.image;
        }

        public void SetHat()
        {
            var hat = GameDataManager.GetSelectedHat();
            _hat.sprite = hat.image;
        }
    }
}