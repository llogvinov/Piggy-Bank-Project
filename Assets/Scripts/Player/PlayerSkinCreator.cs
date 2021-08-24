using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinCreator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hatImage;
    [SerializeField] private SpriteRenderer maskImage;

    private void Start() => SetHatAndMask();

    private void SetHatAndMask()
    {
        Hat hat = GameDataManager.GetSelectedHat();
        Mask mask = GameDataManager.GetSelectedMask();

        hatImage.sprite = hat.image;
        maskImage.sprite = mask.image;
    }
}
