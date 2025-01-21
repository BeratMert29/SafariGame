using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Character
{
    [SerializeField] float wallSlidingGravityScale = 0.1f;
    [SerializeField] float maxWallSlideSpeed = 2f;
    [SerializeField] Vector2 wallJumpDashForce;
    [SerializeField] float wallJumpDashDuration;
    private bool isWallSliding;
    private Animator monkeyanimator;

    void Start()
    {
        monkeyanimator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Flip();
        Jump();
        WallSlide();
        animator.SetBool("run", moveX != 0 && !CheckWall());
        if (Input.GetKeyDown(KeyCode.W) && isWallSliding)
        {
            Player.instance.StartCoolDownCouroutine(WallJump());
        }
    }

    private void WallSlide()
    {
        isWallSliding = false;

        if (!CheckWall()) { return; }
        if (rigidbody2.velocity.y > 0) { return; }

        if (moveX >= 0 && CheckWall() && facingRight)
        {
            rigidbody2.gravityScale = 0.1f;
            isWallSliding = true;
        }
        if (moveX <= 0 && !facingRight)
        {
            rigidbody2.gravityScale = 0.1f;
            isWallSliding = true;
        }

        if (isWallSliding && rigidbody2.velocity.y < -maxWallSlideSpeed)
        {
            rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, -maxWallSlideSpeed);
        }
    }


    private IEnumerator WallJump()
    {
        canMove = false;
        canJump = false;
        canFlip = false;

        rigidbody2.gravityScale = 0f;
        rigidbody2.velocity = new Vector2(-transform.localScale.x * wallJumpDashForce.x + (moveX * 4), wallJumpDashForce.y);

        Player.instance.transform.Rotate(0, 180, 0);

        yield return new WaitForSeconds(wallJumpDashDuration);

        Player.instance.transform.Rotate(0, -180, 0);
        canMove = true;
        canJump = true;
        canFlip = true;

        rigidbody2.gravityScale = characterMode.baseGravityScale;
        rigidbody2.velocity = new Vector2(0, rigidbody2.velocity.y);
    }

}
