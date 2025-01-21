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

            // Saldýrý yap
            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    StartCoroutine(AttackRoutine());
                    lastAttackTime = Time.time;
                }
            }

            animator.SetFloat("Speed", 0); // Bu örnekte enemy sabit olduðu için hýz 0
        }
    }

    private void Flip()
    {
        // Oyuncu düþmanýn solunda mý saðýnda mý kontrol et
        bool playerOnRight = player.position.x > transform.position.x;

        // Eðer oyuncunun tarafý ile düþmanýn baktýðý yön eþleþmiyorsa çevir
        if (playerOnRight && transform.localScale.x < 0 || !playerOnRight && transform.localScale.x > 0)
        {
            // X ekseninde ölçeði çevir
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }



    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        animator.SetBool("hit", true);

        // Bekle ve mermiyi fýrlat
        yield return new WaitForSeconds(0.1f);
        Shoot();

        // Animasyonun tamamlanmasýný bekle
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
