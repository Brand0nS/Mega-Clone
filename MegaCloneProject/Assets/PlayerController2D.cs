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
            rigidBody2D.velocity = new Vector2(2, 0); //change velocity by 2

        }
            else if (Input.GetKey("a") || Input.GetKey("left")) //else if key input is a or left arrow key
        {
            rigidBody2D.velocity = new Vector2(-2, 0); //change velocity by -2 (go to the left)
        }
    }
}
