using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Poison : MonoBehaviour
{
    [SerializeField] private float poisonDamage = 10f;

    //Rakip hit alabiliyorsa hasar verme
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit! Health reduced bsadasdasdasy 1.");
            Health enemyHealth = target.gameObject.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ReciveHit(1); // Enemy'nin saðlýðýný azalt
                
            }
            Destroy(gameObject);
        }
    }

}
