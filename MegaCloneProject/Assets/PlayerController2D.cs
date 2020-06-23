using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
  
    private void FixedUpdate()
    {
        if (Input.GetKey("d") || Input.GetKey("right")) //if key input is d or the right arrow key
        {
            rigidBody2D.velocity = new Vector2(2, rigidBody2D.velocity.y); //change velocity by 2
            
            animator.Play("WalkLoopAnim");
            spriteRenderer.flipX = true; //flip the character

        }
            else if (Input.GetKey("a") || Input.GetKey("left")) //else if key input is a or left arrow key
        {
            rigidBody2D.velocity = new Vector2(-2, rigidBody2D.velocity.y); //change velocity by -2 (go to the left)
            
            animator.Play("WalkLoopAnim");
            spriteRenderer.flipX = false; //don't flip the character
        }

        else
        {
            
            animator.Play("IdleAnim");
        }

        if (Input.GetKey("up") || Input.GetKey("space")) //if key input is up or the spacebar key
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x,4); //keep the velocity the same for x but change it for y
            animator.Play("JumpAnim");
            animator.Play("LandAnim");

        }
    }
}
