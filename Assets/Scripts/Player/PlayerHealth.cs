using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Hearts")]
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    
    [Header("Cracks")]
    [SerializeField] private GameObject smallCrack;
    [SerializeField] private GameObject bigCrack;
    [SerializeField] private GameObject crackedPlayer;
 
    public int Health;
    public int NumberOfHearts;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void TakeDamage(int damage)
    {
        Health = Mathf.Max(0, Health - damage);
        
        UpdateHeartsUI();
        UpdateCracks();
    }

    public void AddHeart()
    {
        Health = Mathf.Min(++Health, 3);
        
        UpdateHeartsUI();
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < Health ? fullHeart : emptyHeart;

            hearts[i].enabled = i < NumberOfHearts;
        }
    }
    
    //Connect player's health and cracks
    private void UpdateCracks()
    {
        switch (Health)
        {
            case 3:
            {
                if (smallCrack.activeSelf) 
                    smallCrack.SetActive(false);
                break;
            }
            case 2:
            {
                if (!smallCrack.activeSelf) 
                    smallCrack.SetActive(true);
                if (bigCrack.activeSelf)
                    bigCrack.SetActive(false);
                break;
            }
            case 1:
            {
                if (!bigCrack.activeSelf) 
                    bigCrack.SetActive(true);
                break;
            }
            case 0:
            {
                Instantiate(crackedPlayer, transform.position, transform.rotation);
                gameManager.GameOver();
                Destroy(gameObject);
                break;
            }
        }
    }

}
