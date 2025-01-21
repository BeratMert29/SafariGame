using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Snake : Character
{
    [SerializeField] private float poisonSpeed = 50f;
    [SerializeField] private GameObject poisonPrefab;
    private Vector2 snakePos;
    private bool canFire = true;
    public Transform player;
    public float posionSpeed=25;
    void Update()
    {
        Move();
        Flip();
        Jump();
        shootPoison();
        animator.SetBool("run", moveX != 0);
    }
    //Zehir atma
    private void shootPoison()
    {
        if (poisonPrefab != null && Input.GetMouseButtonDown(0) && canFire == true)
        {
            animator.SetTrigger("hit");
            float direction = transform.localScale.x >= 1 ? 1 : -1;
            snakePos = new Vector2(rigidbody2.transform.position.x + direction, rigidbody2.transform.position.y+.5f);
            GameObject poison = Instantiate(poisonPrefab, snakePos, Quaternion.identity);
            canFire = false;
            Rigidbody2D poisonBody = poison.GetComponent<Rigidbody2D>();
            if (poisonBody != null)
            {
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPosition.z = transform.position.z; // Z eksenini sabit tutmak için, 2D oyunlar için genelde 0 yapýlýr.

                poisonBody.velocity = (mouseWorldPosition - transform.position).normalized* posionSpeed;
                //poisonBody.velocity = (player.position - transform.position).normalized;
            }
            Player.instance.StartCoolDownCouroutine(SpitCoolDown());
        }
    }    
    private IEnumerator SpitCoolDown(){
        yield return new WaitForSeconds(1);
        canFire = true;
    }

}
