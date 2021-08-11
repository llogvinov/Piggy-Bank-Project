using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
 
    [Header("Cracks")]
    [SerializeField] private GameObject smallCrack;
    [SerializeField] private GameObject bigCrack;
    [SerializeField] private GameObject crackedPlayer;
    [Space]
    
    [SerializeField] private GameManager gameManager;
    
    [SerializeField] private int numberOfHearts;
    private int health;

    private void Start()
    {
        health = numberOfHearts;

        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
        UpdateHeartsUI();
        HealthAndCracks();
    }

    public void AddHeart()
    {
        health = Mathf.Min(++health, 3);
        UpdateHeartsUI();
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numberOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void HealthAndCracks()
    {
        if (health == 3)
        {
            if (smallCrack.activeSelf) { smallCrack.SetActive(false); }
        }
        if (health == 2)
        {
            if (!smallCrack.activeSelf) { smallCrack.SetActive(true); }
            if (bigCrack.activeSelf) { bigCrack.SetActive(false); }
        }
        else if (health == 1)
        {
            if (!bigCrack.activeSelf) { bigCrack.SetActive(true); }
        }
        else if (health == 0)
        {
            Instantiate(crackedPlayer, transform.position, transform.rotation);
            gameManager.GameOver();

            Destroy(gameObject);
        }
    }

}
