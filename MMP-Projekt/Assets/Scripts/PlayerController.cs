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
    private bool canMove = false;    

    [SerializeField] float runSpeed = 5.0f;
    [SerializeField] private AudioSource bowSoundEffect;
    [SerializeField] private AudioSource stepSoundEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();        
    }

    void Update()
    {
        // Get movement input from arrow keys or wasd
        horizontal = Input.GetAxisRaw("Horizontal");    // -1 for left or 1 for right 
        vertical = Input.GetAxisRaw("Vertical"); // -1 for down or 1 for up
        movementDirection = new Vector2(horizontal, vertical).normalized;   // Normalize vector, so that magnitude stays 1 while moving diagonally
        Animate();
    }

    void FixedUpdate()
    {
        // Move the player gameobject - if allowed
        rb.velocity = canMove ? movementDirection * runSpeed : Vector2.zero; // by using force
        // rb.MovePosition((Vector2)transform.position + Time.deltaTime * runSpeed * movementDirection); // by using positioning
    }

    // Adjust animation state
    private void Animate()
    {        
        if (Input.GetKey(KeyCode.L)) // Listen for attack button input
        {
            anim.SetBool("isAttackingBow", true);     // Change animation state to "Combat"
            canMove = false;    // Disable player movement - player should not be moving while attacking
        }
        else if (Input.GetKey(KeyCode.K)) // Listen for attack button input
        {
            anim.SetBool("isAttackingShovel", true);     // Change animation state to "Combat"
            canMove = false;    // Disable player movement - player should not be moving while attacking
        }
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
        anim.SetBool("isAttackingShovel", false); // Change animation state from "Combat" to "Movement"
        anim.SetBool("isAttackingBow", false); // Change animation state from "Combat" to "Movement"
        canMove = true;     // Enable player movement        
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        //now the enemies are getting killed by moving into them
        //the weapon e.g. bow/arrow needs to be a standalone collision object so this can be checked within the arrow monobehaviour
        if(collision.gameObject.TryGetComponent<EnemyAI>(out EnemyAI enemyComponent))
        {
            enemyComponent.TakeDamage(1);
        }
    }

    private void PlayBowSound()
    {
        bowSoundEffect.Play();

    }

    private void PlayStepSound()
    {
        stepSoundEffect.Play();
    }
}