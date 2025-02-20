using UnityEngine;

namespace Main.Player
{
    public class PlayerSkinCreator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _hat;
        [SerializeField] private SpriteRenderer _mask;

        public void SetFullSkin()
        {
            SetMask();
            SetHat();
        }

        public void SetMask()
        {
            Mask mask = GameDataManager.GetSelectedMask();
            _mask.sprite = mask.image;
        }

        public void SetHat()
        {
            Hat hat = GameDataManager.GetSelectedHat();
            _hat.sprite = hat.image;
        }
    }
}