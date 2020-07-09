using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
private enum State
    {
        Walking,
        Turning,
        Dead
    }

    private State currentState;

    [SerializeField]
    private float groundCheckDistance, wallCheckDistance, moveSpeed,maxHealth;
    // Walk/Roll State
    private bool isGrounded;
    private bool isNearWall;

    private float currentHealth;

    private GameObject alive;
    private Rigidbody2D aliveRB;
    private Animator turnAnim;

    private int facingDirection, damageDirection;

    private Vector2 movement;
    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRB = alive.GetComponent<Rigidbody2D>();
        facingDirection = 1;
        turnAnim = alive.GetComponent<Animator>(); 
    }


    [SerializeField]
    private Transform 
        groundCheck,
        wallCheck;
    [SerializeField]
    private LayerMask whatIsGround;

    private void Update()
    {
        switch(currentState){
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Turning:
                UpdateTurningState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;

        }
    }
    private void EnterWalkingState()
    {

    }

    private void UpdateWalkingState()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        isNearWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        


        if (!isGrounded || isNearWall)
        {
            //flip the enemy
            Flip();
        }
        else
        {
            movement.Set(moveSpeed * facingDirection, aliveRB.velocity.y);
            aliveRB.velocity = movement;
        }
    }

    private void ExitWalkingState()
    {

    }


    private void EnterTurningState()
    {
        if (isNearWall)
        {
            turnAnim.SetBool("Turning", true);
        }
    }

    private void UpdateTurningState()
    {
        
    }

    private void ExitTurningState()
    {
        turnAnim.SetBool("Turning", true);
    }


    private void EnterDeadState()
    {
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }


    //Other methods
    private void Damage(float[] attackData) //sends parameters about the damage that the enemy does
    {
        currentHealth -= attackData[0];
        if (currentHealth <= 0)
        {
            SwitchState(State.Dead);
        }

    }
    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Turning:
                ExitTurningState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }
        switch (state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Turning:
                EnterTurningState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }
        currentState = state;
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));



    }

}
