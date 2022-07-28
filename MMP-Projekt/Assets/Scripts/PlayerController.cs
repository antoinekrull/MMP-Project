using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D cc;

    public static event Action<PlayerController> OnPlayerDeath;
    public static event Action<PlayerController> OnDamageTaken;

    private Vector2 movementDirection;
    private float horizontal;
    private float vertical;
    private bool canMove = false;

    public int health, maxHealth = 3;
    public bool isDead = false;

    public GameObject arrowPrefab;

    [SerializeField] float runSpeed = 5.0f;
    [SerializeField] private AudioSource bowSoundEffect;
    [SerializeField] private AudioSource stepSoundEffect;
    [SerializeField] private AudioSource hitSoundEffect;
    [SerializeField] private AudioSource gotHitSoundEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CircleCollider2D>();
        anim.SetInteger("health", health);
    }

    void Update()
    {      
        horizontal = Input.GetAxisRaw("Horizontal");    // -1 for left or 1 for right 
        vertical = Input.GetAxisRaw("Vertical"); // -1 for down or 1 for up
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
        if (Input.GetKey(KeyCode.L)) // Listen for attack button input
        {
            anim.SetBool("isAttackingBow", true);     // Change animation state to "Combat"
            canMove = false; // Disable player movement - player should not be moving while attacking          
        }        
        else if (Input.GetKey(KeyCode.K) && !isDead)
        {
            anim.SetBool("isAttackingShovel", true);     
            canMove = false;    
        }

        if (canMove)
        {
            if (movementDirection != Vector2.zero)
            {
                anim.SetFloat("x", horizontal);
                anim.SetFloat("y", vertical);
            }
            anim.SetFloat("speed", movementDirection.magnitude);
        }
    }

    // Method gets called by the last attack animation frame
    private void EndAttack()
    {
        anim.SetBool("isAttackingShovel", false); // Change animation state from "Combat" to "Movement"
        anim.SetBool("isAttackingBow", false); // Change animation state from "Combat" to "Movement"
        canMove = true;     // Enable player movement        
    }


    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log("Punk got hit. Health: " + health);
        anim.SetInteger("health", health); // If health <= 0: death animation state gets activated    
        /* if (health <= 0)
        {
            isDead = true;
            return;
        } */
        isDead = health <= 0;
        canMove = health >= 1;
    }

    private void Die()
    {
        Destroy(gameObject);
        OnPlayerDeath?.Invoke(this);
    }

    private void ShootArrow()
    {
        float x = anim.GetFloat("x");
        float y = anim.GetFloat("y");
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Arrow arrowScript = arrow.GetComponent<Arrow>();

        arrowScript.velocity = new Vector2(x * 15.0f, y * 15.0f);
        arrowScript.player = gameObject;

        arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(y, x) * Mathf.Rad2Deg);
        Destroy(arrow, 5.0f);
    }

    private void PlayBowSound()
    {
        bowSoundEffect.Play();
        ShootArrow();
    }

    private void PlayStepSound() { stepSoundEffect.Play(); }

    private void PlayHitSound() { hitSoundEffect.Play(); }

    private void PlayGotHitSound() { gotHitSoundEffect.Play(); }
}