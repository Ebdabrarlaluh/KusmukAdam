using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator animator;
    private Transform playerTransform;

    public float moveSpeed = 0.1f;
    public int health = 100;

    public float attackRange = 4f; 
    public float attackRate = 1f;
    float nextAttackTime = 0f;

    private bool isDead = false; 

    private float hurtTime = 0f;
    private float hurtWaitTime = 0.5f;
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {

        if (Time.time-hurtTime >= hurtWaitTime)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange )
            {
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                }  
            }
            else
            {
                Walk();
            }
        }

    }


    private void Attack()
    {
        
        animator.SetTrigger("Attack");
        animator.SetBool("IsWalking", false);
        nextAttackTime = Time.time + 1f / attackRate;
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        health -= damage;
        animator.SetTrigger("Hurt");
        animator.SetBool("IsWalking", false);
        hurtTime = Time.time;

        if (health <= 0)
        {
            Die();
        }
    }

    void Walk()
    {
        // Follow the player
        Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        animator.SetBool("IsWalking", true);
        Turn();
    }
    void Turn()
    {
        if (playerTransform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // turn right
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // turn left
        }
    }
    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);

        isDead = true; 
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        this.enabled = false;
    }

    
}