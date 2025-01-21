using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour,IDamagable
{
    public float health=2;
    public GameObject exp;
   
    public void ReciveHit(float damageAmount)
    {
        health -= damageAmount;
      
        if (health <= 0)
        {

            if (gameObject.name == "Drone")
            {
                Instantiate(exp, transform.position, Quaternion.identity);
            }
            /*else
            {
                Debug.LogWarning($"{gameObject.name} öldü ancak patlama efekti atanmamýþ!");
            }*/
            Destroy(gameObject);
        }
    }
}
