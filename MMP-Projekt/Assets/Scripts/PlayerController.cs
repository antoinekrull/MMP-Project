using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Takes and handles input and movement for a player character
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 movementDirection;
    private float horizontal;
    private float vertical;
    private bool canMove = true;    

    [SerializeField] float runSpeed = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();        
    }

    void Update()
    {
        // Get movement input from arrow keys or wasd
        horizontal = Input.GetAxisRaw("Horizontal");    // -1 for left or 1 for right 
        vertical = Input.GetAxisRaw("Vertical");    // -1 for down or 1 for up
        if (Input.GetKey(KeyCode.Space)) // Listen for attack button input
        {
            anim.SetBool("isAttack", true);     // Change animation state to "Combat"
            canMove = false;    // Disable player movement - player should be moving while attacking
        }        
        movementDirection = new Vector2(horizontal, vertical).normalized;   // Normalize vector, so that magnitude stays 1 while moving diagonally        
        Animate();
    }

    void FixedUpdate()
    {
        rb.velocity = canMove ? movementDirection * runSpeed : Vector2.zero; // Move the player gameobject - if allowed
    }

    // Adjust animation state
    private void Animate()
    {
        if (movementDirection != Vector2.zero && canMove)
        {     
            anim.SetFloat("x", horizontal);
            anim.SetFloat("y", vertical);
        }
        anim.SetFloat("speed", movementDirection.magnitude);                        
    }

    // Method gets called by the last frame of the attack animation
    private void EndAttack()
    {
        anim.SetBool("isAttack", false); // Change animation state from "Combat" to "Movement"
        canMove = true;     // Enable player movement        
    }
}