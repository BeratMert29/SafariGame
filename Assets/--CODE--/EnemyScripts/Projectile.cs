using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float launchForce = 10f; // Başlangıç hızı
    public int damage = 1;         // Hasar
    public float lifetime = 5f;    // Yaşam süresi
    public float gravityScale = 1f; // Yerçekimi etkisi
    public GameObject exp;
    private Rigidbody2D rb;

    public void SetTarget(Vector2 targetPosition)
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found!");
            return;
        }

        // Hedefe doğru bir fırlatma yönü hesapla
        Vector2 startPosition = transform.position;
        Vector2 toTarget = targetPosition - startPosition;

        // Yatay hız
        float horizontalSpeed = toTarget.x * launchForce / toTarget.magnitude;

        // Dikey hız: Yerçekimi etkisi hesaba katılır
        float verticalSpeed = Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y * gravityScale) * Mathf.Abs(toTarget.y));

        // Fırlatma vektörü
        Vector2 launchVelocity = new Vector2(horizontalSpeed, verticalSpeed);
        rb.velocity = launchVelocity;
        rb.gravityScale = gravityScale;
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                Instantiate(exp, transform.position, Quaternion.identity);
                player.ReciveHit(damage);
            }
            Destroy(gameObject);
        }

        if (collision.CompareTag("Jumpable"))
        {

            Instantiate(exp,transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}


