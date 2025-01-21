using System.Collections;
using UnityEngine;

public class MonkeyAttack : MonoBehaviour
{
    [SerializeField] private float attackRange, damage;
    private bool canAttack = true;
    private Vector2 direction;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            monkeyAttack();
        }
    }

    private void monkeyAttack()
    {
        if (canAttack)
        {
            Vector2 origin = Player.instance.GetComponent<Rigidbody2D>().transform.position;
            direction = transform.localScale.x < 0 ? Vector2.left : Vector2.right;

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, attackRange);

            if (hit.collider != null)
            {
                IDamagable damagable = hit.collider.GetComponent<IDamagable>();

                if (damagable != null)
                {
                    damagable.ReciveHit(damage);
                }
            }
        }
        StartCoroutine(waitForNextAttack());
    }

    IEnumerator waitForNextAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(2f);
    }
}
