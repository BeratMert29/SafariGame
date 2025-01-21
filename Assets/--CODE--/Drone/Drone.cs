using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drone : MonoBehaviour 
{
    private GameObject player;
    public float speed = 5f;
    private float detectionDistance = 25f;
    public GameObject bulletPrefab;
    public float fireInterval = 1f;
    public float minDistanceToPlayer = 7.5f;
    public float maxTrackingRange = 5f; // Maksimum takip mesafesi
    private float health = 2;
    private bool isPlayerDetected = false;
    private bool isTrackingPlayer = false;
    private float fireCooldown = 0f;

    public float patrolDistance = 25f; // Drone'un baþlangýç pozisyonundan ne kadar uzaða gideceði
    private Vector3 startPosition; // Drone'un baþlangýç pozisyonu
    private bool movingRight = true; // Drone'un hareket yönü
    private bool returningToStart = false; // Baþlangýç pozisyonuna dönüyor mu?

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        startPosition = transform.position; // Baþlangýç pozisyonunu kaydet
    }

    private void Update()
    {
        Debug.Log(isTrackingPlayer);
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }

        if (!isTrackingPlayer)
        {
            isPlayerDetected = CheckPlayerInDetection();
            if (isPlayerDetected)
            {
                isTrackingPlayer = true;
            }
        }

        if (isTrackingPlayer)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer > maxTrackingRange)
            {
                // Oyuncuyu býrak ve baþlangýç pozisyonuna dön
                isTrackingPlayer = false;
                returningToStart = true;
            }
            else
            {
                FollowPlayer();
                fireCooldown -= Time.deltaTime;
                if (fireCooldown <= 0f)
                {
                    StartCoroutine(FireBulletAndWait());
                }
            }
        }


        if (returningToStart)
        {
            ReturnToStart();
        }
        else if (!isTrackingPlayer)
        {
            Patrol(); // Drone'un baþlangýç pozisyonuna göre gidip gelmesini saðla
        }
    }

    private void ReturnToStart()
    {
        float distanceToStart = Vector2.Distance(transform.position, startPosition);
        if (distanceToStart > 0.1f)
        {
            Vector2 direction = (startPosition - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
        else
        {
            returningToStart = false; // Baþlangýç pozisyonuna ulaþtý
        }
    }

    private void Patrol()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x >= startPosition.x + patrolDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

            if (transform.position.x <= startPosition.x - patrolDistance)
            {
                movingRight = true;
            }
        }
    }

    private bool CheckPlayerInDetection()
    {
        for (float angle = -135; angle <= -45; angle += 1)
        {
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionDistance, LayerMask.GetMask("Player"));

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private void FollowPlayer()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer > minDistanceToPlayer)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                transform.position += (Vector3)(direction * speed * Time.deltaTime);
            }
        }
    }

    private IEnumerator FireBulletAndWait()
    {
        FireBullet();
        fireCooldown = fireInterval;
        yield return new WaitForSeconds(1f);
    }

    private void FireBullet()
    {
        if (bulletPrefab != null && player != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            plusTrapBullet bulletScript = bullet.GetComponent<plusTrapBullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection(player.transform.position - transform.position);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (float angle = -135; angle <= -45; angle += 1)
        {
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction * detectionDistance);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPosition + Vector3.right * patrolDistance, startPosition - Vector3.right * patrolDistance);
    }

    
}
