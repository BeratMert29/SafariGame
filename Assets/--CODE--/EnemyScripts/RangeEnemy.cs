using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Animator animator;

    public float attackRange = 10f;
    public float attackCooldown = 2f;
    public float launchForce = 5f;
    public float gravityScale = 1f;
    public float health = 1f;
    private Transform player;
    private float lastAttackTime = 0;
    public float speed = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    animator.SetTrigger("hit");
                    Shoot();
                    lastAttackTime = Time.time;
                }
                else
                {
                    animator.SetBool("hit", false);
                }
            }
            animator.SetFloat("Speed", Mathf.Abs(speed));

            // Player ile karakterin pozisyonunu karşılaştır
            if(player.position.x>transform.position.x)
            {
                transform.localScale=new Vector3(-1,1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void Shoot()
    {
        // Mermiyi instantiate et
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Rigidbody2D bileşenini al
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Player'ın konumunu al
             // Player'ın tag'ı "Player" olmalı
            if (player != null)
            {
                Vector2 direction = (player.position - transform.position).normalized; // Yön vektörü

                // Mermiyi bu yönde fırlat
                rb.velocity = direction * launchForce;

                // Yerçekimini devre dışı bırak (isteğe bağlı)
                rb.gravityScale = 0;
            }
        }
    }

}

