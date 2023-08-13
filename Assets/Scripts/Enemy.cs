using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 0.1f;
    public int health = 100;
    Rigidbody2D rb;
    public Animator animator;
    private bool isDead = false; // Yaratığın ölüp ölmediğini takip eden değişken
    private Transform playerTransform;
    public float attackRange = 4f; // Oyuncuya saldırmak için yaklaşma mesafesi
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Oyuncunun X koordinatına doğru hareket et
        Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (playerTransform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Sağa dön
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Sola dön
        }

       
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange )
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                Debug.Log("sad");
            }

            else 
                animator.SetBool("IsAttacking", false);

        }
        else
        {
            // Saldırı yapmıyorsa, saldırı animasyonunu durdur
            animator.SetBool("IsAttacking", false);
        }

    }

    private void Attack()
    {
        // Saldırı animasyonunu çalıştır
        animator.SetBool("IsAttacking", true);

        // Saldırı işlemleri burada yapılabilir
    }

    public void TakeDamage(int damage)
    {
        if (isDead) // Eğer yaratık öldüyse artık zarar almayı durdur
            return;

        health -= damage;
        animator.SetTrigger("Hurt");
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        isDead = true; // Yaratığın öldüğünü işaretle
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        this.enabled = false;
        //GetComponent<Rigidbody2D>().gravityScale = 0
    }

    
}