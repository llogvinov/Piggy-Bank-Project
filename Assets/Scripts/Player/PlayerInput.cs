using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public float HorizontalInput;
    
    private bool moveLeft;
    private bool moveRight;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.IsGameOver)
            return;
        
        GetPlayerInput();
    }

    //Move player with buttons 
    public void TouchDownLeft() => moveLeft = true;
    public void TouchUpLeft() => moveLeft = false; 
    public void TouchDownRight() => moveRight = true; 
    public void TouchUpRight() => moveRight = false; 
    
    private void GetPlayerInput()
    {
        if (moveLeft)
            HorizontalInput = -1f;
        else if (moveRight)
            HorizontalInput = 1f;
        else
            HorizontalInput = 0f;
    }
}
