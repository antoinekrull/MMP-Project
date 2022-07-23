using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyAI : MonoBehaviour
{

    public int health, maxHealth = 1;
    public static event Action<EnemyAI> OnEnemyKilled;
    public static event Action<EnemyAI> OnDamageTaken;

    public float speed = 3.0f;
    public float checkRadius;
    public float attackRadius;

    public LayerMask layer; // selectable layer mask

    private GameObject playerGameObject;
    private Transform target; // Punk Player
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;

    PlayerController player;

    private bool isInChaseRange;
    private bool isInAttackRange;
    private bool canMove = true;

    private float stopwatch = 0f;

    private BoxCollider2D playersBoxCollider;
    private CircleCollider2D playersShovelCollider;
    private BoxCollider2D enemysBoxCollider;
    private CircleCollider2D circleCollider;
    private System.Random ran = new System.Random();

    [SerializeField] private AudioSource stepSoundEffect;
    [SerializeField] private AudioSource hitSoundEffect;
    [SerializeField] private AudioSource gotHitSoundEffect;


    private void Start()
    {
        playerGameObject = GameObject.FindWithTag("Player");
        player = playerGameObject.GetComponent<PlayerController>();
        playersBoxCollider = player.GetComponent<BoxCollider2D>();
        enemysBoxCollider = GetComponent<BoxCollider2D>();
        playersShovelCollider = player.GetComponent<CircleCollider2D>();
        health = maxHealth;        
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = playerGameObject.transform;
    }

    private void Update()
    {
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, layer);    // Checks if Enemys should run after the player
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, layer);  // Checks if Enemys are close enough to attack the player

        if (!isInChaseRange && !isInAttackRange) // if enemy is too far from player to chase/attack:
        {
            speed = 0.6f; // set speed slower because enemy is not chasing after player but taking a nice smooth walk
            anim.SetFloat("animationSpeed", 0.4f);
            TakeAWalk();
        }
        else
        {
            speed = 3f; // set speed faster because enemy is running after player
            anim.SetFloat("animationSpeed", 1f);
            movement = (target.position - transform.position).normalized; // get movement direction towards player
        }

        Animate();
        VarifyHitFromPlayer();
    }

    // set animation state
    private void Animate()
    {
        if (isInAttackRange)
        {
            anim.SetBool("isAttacking", true);
            canMove = false; // Disable player movement - player should not be moving while attacking
        }

        else if (canMove) // check canMove because the enemy can go back into ChaseRange while still doing an attack!
        {
            if (movement != Vector2.zero)
            {
                anim.SetFloat("x", movement.x);
                anim.SetFloat("y", movement.y);
            }
            anim.SetFloat("speed", movement.magnitude); // toggle between idle and walking animation
        }
    }

    private void VarifyHitFromPlayer()
    {
        if (enemysBoxCollider.IsTouching(playersShovelCollider) && !player.isDead)
        {
            TakeDamage(1);
        }
    }

    private void VarifyHitAgainstPlayer()
    {               

        if (circleCollider.IsTouching(playersBoxCollider) && !player.isDead)
        {
            player.TakeDamage(1);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = canMove ? movement * speed : Vector2.zero;
    }

    public void TakeDamage(int damageAmount)
    {
        if(gameObject != null)
        {
            health -= damageAmount;
            canMove = health >= 1;
            PlayGotHitSound();

            anim.SetInteger("health", health); // If health <= 0: death animation state gets activated               
        }            
        anim.SetInteger("health", health); // If health <= 0: death animation state gets activated                    
    }

    // Method  gets called by last frame of death animation
    public void Die()
    {
        Destroy(gameObject);
        OnEnemyKilled?.Invoke(this);
    }

    // RandomWalk
    private void TakeAWalk()
    {
        // every few seconds we set a new random direction to walk into        
        stopwatch += Time.deltaTime;
        if (stopwatch > ran.Next(2, 6))
        {
            stopwatch = 0f;
            movement = new Vector2(ran.Next(-1, 2), ran.Next(-1, 2)).normalized;
        }
    }

    // Method gets called by last frame of the attack animation
    private void EndAttack()
    {
        anim.SetBool("isAttacking", false);
        canMove = true;
        if (movement != Vector2.zero)
        {
            anim.SetFloat("x", movement.x);
            anim.SetFloat("y", movement.y);
        }
    }

    private void PlayStepSound()
    {
        stepSoundEffect.Play();
    }

    private void PlayHitSound()
    {
        hitSoundEffect.Play();
    }

    private void PlayGotHitSound()
    {
        gotHitSoundEffect.Play();
    }

}