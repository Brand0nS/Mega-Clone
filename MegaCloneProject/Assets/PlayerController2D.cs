using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;
    bool isGrounded;
    bool canMove;
    public float doubleTapTime = 0.5f; //tapping the dash button within half a second will trigger this.
    public float dashWaitTime = 2.0f; // must wait 2 seconds inbetween every dash.
    // Time that the dash button was last pressed
    private float _lastDashButtonTime;
    // Time of the last dash
    private float _lastDashTime;
    [SerializeField]
    Transform GroundCheck;

    private float walkSpeed = 1.5f; //variable for walking speed.
    private float jumpHeight = 5; //variable for jumping height.
    private float dashSpeed = 3.0f; //variable for dashing speed.

    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
     
    }

    bool canDash //can the character dash?
    {
        get
        {
            return Time.time - _lastDashTime > dashWaitTime;
        }
    }

    void InitiateDash()
    {
        _lastDashTime = Time.time;
        

        // blah...
    }
    // Update is called once per frame

    private void FixedUpdate()
    {
        if (canDash && (Input.GetKey("d") || Input.GetKey("left")))
        {
            // If second time pressed?
            if (Time.time - _lastDashButtonTime < doubleTapTime)
            {
                InitiateDash();
                _lastDashButtonTime = Time.time;
                rigidBody2D.velocity = new Vector2(3, rigidBody2D.velocity.y); //change velocity by 4
                animator.Play("DashAnim");
                spriteRenderer.flipX = true; // flip the character
            }

            else if (canDash && (Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow)))
            {
                if (Time.time - _lastDashButtonTime < doubleTapTime)
                {
                    InitiateDash();
                    _lastDashButtonTime = Time.time;
                    rigidBody2D.velocity = new Vector2(-3, rigidBody2D.velocity.y); //change velocity by -4
                    animator.Play("DashAnim");
                    spriteRenderer.flipX = false; //don't flip the character
                }
            }
        }
        if (Physics2D.Linecast(transform.position,GroundCheck.position, 1 << LayerMask.NameToLayer("Foreground"))) //if Linecast goes from player to foreground object,
        {                                                                       //and it hits the Foreground Layer, then
            isGrounded = true;                                                  //return true
        }
        else
        {
            isGrounded = false;
        }
        
 
        if (Input.GetKey("d") || Input.GetKey("right")) //if key input is d or the right arrow key
        {
            rigidBody2D.velocity = new Vector2(walkSpeed, rigidBody2D.velocity.y); //change velocity by 2
            if (isGrounded) 
            {
                animator.Play("WalkLoopAnim");
                spriteRenderer.flipX = true; //flip the character
            }

            if (Input.GetKey("right"))
            {

            }
            

        }
            else if (Input.GetKey("a") || Input.GetKey("left")) //else if key input is a or left arrow key
        {
            rigidBody2D.velocity = new Vector2(-walkSpeed, rigidBody2D.velocity.y); //change velocity by -2 (go to the left)

            if (isGrounded)
            {
                animator.Play("WalkLoopAnim");
                spriteRenderer.flipX = false; //don't flip the character
            }
        }

        else
        {
            if (isGrounded) //if the player is grounded, play the idle animation
            {
                animator.Play("IdleAnim");
                rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
            }
        }

        if (Input.GetKey("space")&& isGrounded) //if the spacebar key is pressed and character is grounded
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x,jumpHeight); //keep the velocity the same for x but change it for y
            animator.Play("JumpAnim");
            

        }
    }
}
