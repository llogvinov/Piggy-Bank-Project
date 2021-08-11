using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    public float HorizontalInput;

    private bool moveLeft;
    private bool moveRight;

    private void Start()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        GetPlayerInput();
    }

    //Move player with buttons 
    public void TouchDownLeft() { moveLeft = true; }
    public void TouchUpLeft() { moveLeft = false; }
    public void TouchDownRight() { moveRight = true; }
    public void TouchUpRight() { moveRight = false; }

    public void GetPlayerInput()
    {
        if (moveLeft)
        {
            HorizontalInput = -1f;
        }
        else if (moveRight)
        {
            HorizontalInput = 1f;
        }
        else
        {
            HorizontalInput = 0f;
        }
    }
}
