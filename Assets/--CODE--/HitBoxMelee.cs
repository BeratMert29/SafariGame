using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxMelee : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player ile �arp��ma: ");
            Player player = collision.GetComponent<Player>();
           /* if (player != null && !isAttacking)
            {
                Debug.Log("Coroutine ba�lat�l�yor...");
                StartCoroutine(Attack(player)); // Sald�r� animasyonunu ba�lat
            }*/
        }
    }
}
