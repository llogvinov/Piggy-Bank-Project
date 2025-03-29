using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public float HorizontalInput;
    
    private bool moveLeft;
    private bool moveRight;

    private void Update()
    {
        GetPlayerPCInput();
        //GetPlayerInput();
    }

    private void GetPlayerPCInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
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
