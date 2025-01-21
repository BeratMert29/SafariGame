using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Component References
    [SerializeField] protected CharacterMode characterMode;
    protected Rigidbody2D rigidbody2;
    protected Transform playerTransform;
    protected Animator animator;

    //public
    [HideInInspector] public float damageReduction;
    //stats
    protected float speed;
    protected float jumpForce;
    protected float jumpRayDistance;
    protected bool facingRight = true;
    protected float baseGravityScale;
    protected float fallGravityScale;
    protected float maxFallSpeed;
    //input
    protected float moveX;

    //booleans
    public bool canMove;
    protected bool canJump;
    protected bool canFlip;

    //private
    private float wallRayDistance = 0.55f;

    public void Initilize()
    {
        rigidbody2 = Player.instance.gameObject.GetComponent<Rigidbody2D>();
        playerTransform = Player.instance.gameObject.GetComponent<Transform>();

        animator = GetComponent<Animator>();

        rigidbody2.gravityScale = 2;

        transform.position = Player.instance.transform.position;

        this.speed = characterMode.speed;
        this.jumpForce = characterMode.jumpForce;
        this.jumpRayDistance = characterMode.jumpRayDistance;
        this.damageReduction = characterMode.damageReduction;
        this.baseGravityScale = characterMode.baseGravityScale;
        this.fallGravityScale = characterMode.fallGravityScale;
        this.maxFallSpeed = characterMode.maxFallSpeed;

        canMove = true;
        canJump = true;
        canFlip = true;
    }
    public virtual void InitilizeChange()
    {
        //Debug.Log("Changed to " + characterMode.modeName);
    }

    protected virtual void Jump(bool noNeedGroundCheck = false)
    {
        if (!canJump)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (CheckGround() || noNeedGroundCheck)
            {
                rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, jumpForce);
            }
        }

        AirControl();
    }

    protected void AirControl()
    {
        if (rigidbody2.velocity.y > 0 && Input.GetKey(KeyCode.W))
        {
            rigidbody2.gravityScale = baseGravityScale;
        }

        if (rigidbody2.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rigidbody2.gravityScale = fallGravityScale * 3;
        }

        if (rigidbody2.velocity.y < 0)
        {
            rigidbody2.gravityScale = fallGravityScale;
        }
        if (rigidbody2.velocity.y < -maxFallSpeed)
        {
            rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, -maxFallSpeed);
        }
    }

    protected bool CheckGround()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, jumpRayDistance);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.tag == "Jumpable")
                {
                    return true;
                }
            }
        }

        return false;
    }

    protected bool CheckWall()
    {
        Vector2 direction = Vector2.right;
        if (!facingRight) { direction = Vector2.left; }

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, wallRayDistance, 1 << 6);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                return true;

            }
        }

        return false;
    }


    protected virtual void Move()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        if (!canMove) { return; }
        if (CheckWall()) { return; }

        //rigidbody2.velocity = new Vector2(moveX * speed, rigidbody2.velocity.y);

        playerTransform.Translate(Vector2.right * speed * Time.deltaTime * moveX);
    }

    protected virtual void Move(GameObject arms, bool webing)
    {

        moveX = Input.GetAxisRaw("Horizontal");

        //if (!canMove) { return; }
        if (CheckWall()) { return; }

        //rigidbody2.velocity = new Vector2(moveX * speed, rigidbody2.velocity.y);
        if (webing)
        {
            playerTransform.position = new Vector3(arms.transform.position.x, arms.transform.position.y, playerTransform.position.z);

        }
        else
        {
            playerTransform.Translate(Vector2.right * speed * Time.deltaTime * moveX);
        }
    }

    protected void Flip()
    {
        if (!canFlip) { return; }
        if (facingRight && moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = !facingRight;
        }
        else if (!facingRight && moveX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = !facingRight;
        }
    }

    protected void DrawDebug()
    {
        Debug.DrawRay(transform.position, Vector2.right * wallRayDistance, Color.red);
    }
}
