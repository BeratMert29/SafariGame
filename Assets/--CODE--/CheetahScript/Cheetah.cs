using System.Collections;
using System.Security.Cryptography;
using UnityEditor.Callbacks;
using UnityEngine;


public class Cheetah : Character
{
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] float dashingPower = 24f;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] float dashingCooldown = 1f;
    
    void Update()
    {
        Flip();
        Move();
        Jump();

        animator.SetBool("run", moveX != 0);
        if (Input.GetMouseButtonDown(0) && canDash)
        {
            Player.instance.StartCoolDownCouroutine(Dash());
        }
    }

    private IEnumerator Dash(){
        canMove = false;
        canJump = false;
        canDash = false;
        
        isDashing = true;
        rigidbody2.gravityScale = 0f;
        rigidbody2.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        
        canMove =  true;   
        canJump = true;

        rigidbody2.velocity = new Vector2(0,0f);
        rigidbody2.gravityScale = characterMode.baseGravityScale;
        
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}