using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFire : MonoBehaviour
{
    public float speed=20f;
    public int damage = 50;
    public Rigidbody2D rb;

    
    Vector2 DragStartPos;


    void Start()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {

        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (DragEndPos - DragStartPos) * speed;
            rb.velocity = _velocity;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy= collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

}
