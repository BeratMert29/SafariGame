using System.Collections;
using UnityEngine;

public class BossHit1 : MonoBehaviour, IState
{
    private Boss boss;
    private Player player;
    private Vector3 target;
    private int tentacles;


    public BossHit1(Boss boss)
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
        while(t < 6){
            for(int i = 0; i < 5;i++){
                boss.SpawnTentacle(new Vector2(boss.transform.position.x + (t%2 * 2f) + (i+1 ) * 4.4f,boss.transform.position.y- 1.5f));
                boss.SpawnTentacle(new Vector2(boss.transform.position.x - (t%2 * 2f) - (i+1) * 4.4f,boss.transform.position.y- 1.5f));
            }
            for(int i = 0; i < 4;i++){
                boss.SpawnTentacle(new Vector2(boss.transform.position.x + 12.5f + (i+1) * 2.2f,boss.transform.position.y + 4.5f));
                boss.SpawnTentacle(new Vector2(boss.transform.position.x - 12.5f - (i+1) * 2.2f,boss.transform.position.y + 4.5f));
            }
            yield return new WaitForSeconds(5f);
            t++;
        }
        yield return new WaitForSeconds(4f);
        boss.hitDone = true;

    }
}
