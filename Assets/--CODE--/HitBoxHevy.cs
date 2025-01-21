using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxHevy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player ile çarpýþma: ");
            Player player = collision.GetComponent<Player>();
            Debug.Log(player);
            if (player != null)
            {
               
                player.ReciveHit(2);
            }
        }
    }
}
