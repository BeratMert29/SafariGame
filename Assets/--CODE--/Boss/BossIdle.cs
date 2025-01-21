using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BossIdle : IState
{
    Boss boss;
    public BossIdle(Boss boss)
    {
        this.boss = boss;
    }

    public void OnEnter()
    {
        boss.randomIndex  = -1;
        boss.hitDone = false;
        boss.animator.Play("BossIdle");
        boss.StartCoroutine(wait());
      
    }

    public void OnExit()
    { 
      
    }

    public void Tick()
    {
    }
    IEnumerator wait()
    {
        yield return  new WaitForSeconds(5f);
        boss.randomHit();
    }
}
