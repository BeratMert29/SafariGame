using System.Collections;
using UnityEngine;

public class BossHit2 : IState
{   
  Boss boss;
  Vector2 target = new Vector2(0, 1);
  float alpha =2f;
  public BossHit2(Boss boss)
  {
    this.boss = boss;
  }
  public void OnEnter()
  {
    boss.StartCoroutine(TentacleAttack());
  }
  public void OnExit()
  {
  }

  public void Tick()
  {
  }
  private IEnumerator TentacleAttack()
    {
        boss.animator.Play("BossPump");
        yield return new WaitForSeconds(1f);
        int t = 0;
        while(t < 11){
            boss.SpawnTentacle(new Vector2(boss.transform.position.x + 1 + (t+1) * 2.2f,boss.transform.position.y- 1.5f));
            boss.SpawnTentacle(new Vector2(boss.transform.position.x - 1 - (t+1) * 2.2f,boss.transform.position.y- 1.5f));
            yield return new WaitForSeconds(0.4f);
            t++;
        }
        yield return new WaitForSeconds(3f);
        boss.hitDone = true;

    }
}
