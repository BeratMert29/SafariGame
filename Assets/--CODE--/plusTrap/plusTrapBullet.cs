using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plusTrapBullet : MonoBehaviour
{
    private float speed = 25f;
    public int damage = 1;
    public float lifetime = 2f;
    private Vector2 moveDirection;

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized; 
    }

    private void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    private void Update()
    {
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.ReciveHit(damage);
            }
            Destroy(gameObject); 
        }
        else if (!(collision.gameObject.CompareTag("Bullet"))&& !(collision.gameObject.CompareTag("BulletTrap"))&&!(collision.gameObject.CompareTag("Enemy")))
        {
            Debug.Log("Çarpýþma algýlandý: " + collision.gameObject.name);
            
            Destroy(gameObject);
        }

    }
}
