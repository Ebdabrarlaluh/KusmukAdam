using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    Rigidbody2D rb;
    public Animator animator;
    private bool isDead = false; // Yaratığın ölüp ölmediğini takip eden değişken

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

        //GetComponent<Rigidbody2D>().gravityScale = 0
    }

    
}