using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberScript : MonoBehaviour
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
    private bool isAttacking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            Flip();

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Sald�r� yap
            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    StartCoroutine(AttackRoutine());
                    lastAttackTime = Time.time;
                }
            }

            animator.SetFloat("Speed", 0); // Bu �rnekte enemy sabit oldu�u i�in h�z 0
        }
    }

    private void Flip()
    {
        // Oyuncu d��man�n solunda m� sa��nda m� kontrol et
        bool playerOnRight = player.position.x > transform.position.x;

        // E�er oyuncunun taraf� ile d��man�n bakt��� y�n e�le�miyorsa �evir
        if (playerOnRight && transform.localScale.x < 0 || !playerOnRight && transform.localScale.x > 0)
        {
            // X ekseninde �l�e�i �evir
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }



    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        animator.SetBool("hit", true);

        // Bekle ve mermiyi f�rlat
        yield return new WaitForSeconds(0.1f);
        Shoot();

        // Animasyonun tamamlanmas�n� bekle
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        while (stateInfo.IsName("Attack") && stateInfo.normalizedTime < 1.0f)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        animator.SetBool("hit", false);
        isAttacking = false;
    }

    private void Shoot()
    {
        if (player == null) return;

        // Mermiyi instantiate et
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projScript = projectile.GetComponent<Projectile>();

        if (projScript != null)
        {
            // Merminin hedefini oyuncunun pozisyonuna ayarla
            projScript.SetTarget(player.position);
        }

        Debug.Log("Enemy fired!");
    }


    public void ReciveHit(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
