using UnityEngine;

public class PlayerComputerInput : PlayerInput
{
    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
    }
}
