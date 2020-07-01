using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    public float healthBar;
    [SerializeField]
    public float maxHealth;
    [SerializeField]
    int amountOfLives;  
    bool isGrounded, canJump;
    bool canMove;
    int facingDirection = 1;    
    public float doubleTapTime = 1.0f; //tapping the dash button within a second will trigger this.
    public float dashWaitTime = 0.75f; // must wait 0.75 seconds inbetween every dash.
    // Time that the dash button was last pressed
    private float _lastDashButtonTime;
    // Time of the last dash
    private float _lastDashTime;
    [SerializeField]
    Transform GroundCheck;
    [SerializeField]
    private float walkSpeed = 1.5f; //variable for walking speed.
    [SerializeField]
    private float jumpHeight = 5; //variable for jumping height.
    [SerializeField]
    private float dashSpeed = 3.0f; //variable for dashing speed.

    private float lastImageXpos;

    public float distanceBetweenImages;

    // Added by Ricardo Guerra
    // Is the character currently dashing or not?
    private bool isDashing = false;

    // The current dashing speed.
    private float currentDashSpeed;

    // Should the character stop dashing?
    private bool stopDashing = false;
    // End of Ricardo's code.

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHealth = 100f;
        healthBar = maxHealth;
        amountOfLives = 5;
    }

    bool canDash //can the character dash?
    {
        get
        {
            return Time.time - _lastDashTime > dashWaitTime;
        }
    }

   
        
    /* Added by Ricardo Guerra
     * Method name: Dash
     * Type: void
     * Description: Makes the character dash in response to pressing the dash button.
     */
    void Dash()
    {
        if (canDash)
        {
            _lastDashTime = Time.time;
            _lastDashButtonTime = Time.time;
            isDashing = true;
        }

        if (spriteRenderer.flipX)
        {
            currentDashSpeed = dashSpeed;
        }
        else
        {
            currentDashSpeed = -1 * dashSpeed;
        }
        rigidBody2D.velocity = new Vector2(currentDashSpeed, rigidBody2D.velocity.y); //change velocity by 4
        animator.Play("DashAnim");
        AfterImagePool.Instance.GetFromPool(); //initiates the afterimages, (Gets the data from the pool)
        lastImageXpos = transform.position.x; //sets the last image x position to the position of the character.

    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }
    // End of Ricardo's code.

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Added by Ricardo Guerra
        // canDash becomes true when the dash is complete, and then another dash can be performed.
        // If you are still holding the dash button down when the dash is complete...        
        if (isDashing && canDash)
        {
            // Make the character stop dashing.
            isDashing = false;
            stopDashing = true;

            if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
            {
                AfterImagePool.Instance.GetFromPool();
                lastImageXpos = transform.position.x; 
            }

            
        }
        
        // Dash in the direction that the character is facing.
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (!stopDashing && isGrounded) Dash();
        }

        // Else... when you let go of the dash button, stop dashing and you can dash again.
        else
        {
            isDashing = false;
            stopDashing = false;
        }
        // End of Ricardo's code.
    

        //This is the Groundcheck that allows the character to Jump.
        if (Physics2D.Linecast(transform.position,GroundCheck.position, 1 << LayerMask.NameToLayer("Foreground"))) //if Linecast goes from player to foreground object,
        {                                                                       //and it hits the Foreground Layer, then
            isGrounded = true;                                                  //return true
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKeyDown("down") || Input.GetKeyDown("s")) //Basic Crouch Functionality
        {
            canMove = false;
            canJump = false;
            
            animator.Play("CrouchAnim");
        }

        if (Input.GetKey("d") || Input.GetKey("right")) //if key input is d or the right arrow key
        {
            // Edited by Ricardo Guerra
            // This line of code should not go into of the isGrounded if statement,
            // if you want the character's sprite to flip while jumping as well.
            spriteRenderer.flipX = true; // Flip the sprite for the character.

            // Edited by Ricardo Guerra
            // This should only happen when the character is not dashing.
            if (!isDashing)
            {
                rigidBody2D.velocity = new Vector2(walkSpeed, rigidBody2D.velocity.y); //change velocity by 2

                if (isGrounded)
                {
                    animator.Play("WalkLoopAnim");
                }
            }
        }
        else
        {
            if (Input.GetKey("a") || Input.GetKey("left")) //else if key input is a or left arrow key
            {
                // Edited by Ricardo Guerra
                // This line of code should not go into of the isGrounded if statement,
                // if you want the character's sprite to flip while jumping as well.
                spriteRenderer.flipX = false; // Flip the sprite for the character.

                // Edited by Ricardo Guerra
                // This should only happen when the character is not dashing.
                if (!isDashing)
                {
                    rigidBody2D.velocity = new Vector2(-walkSpeed, rigidBody2D.velocity.y); //change velocity by -2 (go to the left)

                    if (isGrounded)
                    {
                        animator.Play("WalkLoopAnim");
                    }
                }
            }

            else
            {
                // Edited by Ricardo Guerra
                // If the player is grounded and is not dashing, play the idle animation.
                if (isGrounded && !isDashing)
                {
                    animator.Play("IdleAnim");
                    rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
                }
            }
        }

        // Edited by Ricardo Guerra
        // Using Input.GetKeyDown is more optimal.
        // The character should not keep jumping while the spacebar key is held down.
        // If the spacebar key is pressed (not held down) and if the character is grounded...
        if (Input.GetKeyDown("space") && isGrounded)
        {
           
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x,jumpHeight); //keep the velocity the same for x but change it for y
            animator.Play("JumpAnim");
        }

      
    }
}