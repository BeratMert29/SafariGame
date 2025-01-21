using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHit : MonoBehaviour, IState
{
    Boss boss;
    public BossHit(Boss boss)
    {
        this.boss = boss;
    }

    public void OnEnter()
    {
        boss.StartCoroutine(RangedAttack());
    }

    public void OnExit()
    {

    }

    public void Tick()
    {
    }
    IEnumerator RangedAttack(){
        boss.animator.Play("BossSpell");
        yield return new WaitForSeconds(0.8f);
        boss.SpawnBall((Vector2)boss.transform.position + Vector2.up * 3f);
        boss.hitDone = true;
    }
}
