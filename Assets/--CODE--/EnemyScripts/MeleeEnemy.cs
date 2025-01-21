using System.Collections;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public Animator animator;
    public float speed = 2f;
    public int damage = 1;
    public float limit;
    private float leftLimit;
    private float rightLimit;
    public float health = 2f;
    private Transform target;
    private bool movingRight = true;
    private bool isAttacking = false;
    private bool isDashing = false;

    public float dashSpeed = 10f;
    public float dashDuration = 0.5f;
    private float originalSpeed;

    private void Awake()
    {

        leftLimit = transform.position.x - limit;
        rightLimit = transform.position.x + limit;
        transform.localScale = new Vector3(-1, 1, 1);
        originalSpeed = speed;
    }

    private void Update()
    {
        if (isAttacking || isDashing) return;

        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null && !isAttacking)
            {
                StartCoroutine(Attack(player));
            }
        }
    }

    private IEnumerator Attack(Player player)
    {
        isAttacking = true;
        animator.SetBool("hit", true);

        yield return PerformDash(new Vector2(target.position.x,transform.position.y));

        if (player != null)
        {
            player.ReciveHit(damage);
        }

        AnimatorStateInfo stateInfo;
        do
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        } while (stateInfo.IsName("meleHit") && stateInfo.normalizedTime < 1.0f);

        animator.SetBool("hit", false);
        isAttacking = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopAllCoroutines();
            animator.SetBool("hit", false);
            isAttacking = false;
        }
    }

    private IEnumerator PerformDash(Vector2 targetPosition)
    {
        isDashing = true;

        // Hedefe doğru yön vektörünü hesapla
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Dash hareketi
        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            transform.Translate(direction * dashSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Dash sonrası hız sıfırlanır
        isDashing = false;
    }
}
