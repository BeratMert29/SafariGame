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
            Debug.Log("Player ile çarpýþma: ");
            Player player = collision.GetComponent<Player>();
           /* if (player != null && !isAttacking)
            {
                Debug.Log("Coroutine baþlatýlýyor...");
                StartCoroutine(Attack(player)); // Saldýrý animasyonunu baþlat
            }*/
        }
    }
}
