using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float minTimeCondition;
    [SerializeField] private float maxTimeCondition;
    [SerializeField] private int condition;
    [SerializeField] private float xFlipPosition = 1.4f;

    private float timeCondition;
    private int conditionsCount = 2;
    private bool facingRight = false;
    private Vector2 moveX;

    private Rigidbody2D playerRigitbody;
    private Animator playerAnimator;

    private void Awake()
    {
        playerRigitbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            //Randomise player's condition (idle, moving)
            condition = Random.Range(1, conditionsCount++);
            timeCondition = Random.Range(minTimeCondition, maxTimeCondition);

            if (condition == 1)
            {
                PlayerIdle();
                yield return new WaitForSeconds(timeCondition);
            }
            else if (condition == 2)
            {
                PlayerMoving();
                yield return new WaitForSeconds(timeCondition);
                playerRigitbody.velocity = Vector2.zero;
            }
        }
    }

    private void PlayerIdle()
    {
        playerAnimator.SetFloat("Speed", 0);
    }

    private void PlayerMoving()
    {
        playerAnimator.SetFloat("Speed", 1);
        Move();
    }

    //Keeps the pig on the screen
    private void Move()
    {
        if (transform.position.x < -xFlipPosition)
        {
            if (!facingRight)
            {
                Flip();
                
            }
            moveX = Vector2.right * playerSpeed;
            playerRigitbody.velocity = moveX;
        }
        else if (transform.position.x > xFlipPosition)
        {
            if (facingRight)
            {
                Flip();
            }
            moveX = Vector2.left * playerSpeed;
            playerRigitbody.velocity = moveX;
        }
        else
        {
            moveX = RandomDirection() * playerSpeed;
            if (moveX.x < 0 && facingRight || moveX.x > 0 && !facingRight) 
            { 
                Flip(); 
            }
            playerRigitbody.velocity = moveX;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private Vector2 RandomDirection()
    {
        int direction = Random.Range(0, 2);
        if (direction == 0) 
        { 
            return Vector2.left; 
        }
        else 
        { 
            return Vector2.right; 
        }
    }

}
