using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyAI : MonoBehaviour
{

    public float health, maxHealth = 3f;
    public static event Action<EnemyAI> OnEnemyKilled;

    public float speed;
    public float checkRadius;
    public float attackRadius;

    public bool shouldRotate;

    public LayerMask whatIsPlayer;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private float horizontal;
    private float vertical;

    private bool isInChaseRange;
    private bool isInAttackRange;


    private void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        anim.SetBool("IsRunning", isInChaseRange);  //f�ngt an zu laufen, wenn in ChaseRaius von Player

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);    //wenn player in checkradius, setzt bool auf true
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);  //same nur attackradius

        movement = (target.position - transform.position).normalized;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (shouldRotate)
        {
            anim.SetFloat("X", movement.x);
            anim.SetFloat("Y", movement.y);
        }
    }

    private void FixedUpdate()
    {
        if(isInChaseRange && !isInAttackRange)
        {
            rb.MovePosition((Vector2)transform.position + (speed * Time.deltaTime * movement));
        }
        if (isInAttackRange)
        {
            rb.velocity = Vector2.zero;
        }
        /*if (!isInAttackRange && !isInChaseRange ) save for later
        {

            WalkRandom();
        }*/
    }

    /*private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }
    */

    public void TakeDamage(float damageAmount) {
        Debug.Log("Damage taken");
        health -= damageAmount;
        if(health <= 0) {
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    }

    //RandomWalk
    private void WalkRandom()
    {

        horizontal = DirRandom();
        vertical = DirRandom();
        movement = new Vector2(horizontal, vertical).normalized;
        
        rb.velocity = movement * speed;

    }

    private float DirRandom()
    {
        System.Random rand = new System.Random();
        double min = -1;
        double max = 1;
        double range = max - min;
        
        double sample = rand.NextDouble();
        double scaled = (sample * range) + min;
        float f = (float)scaled;
        return f;

    }
}
