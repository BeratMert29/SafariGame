using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Boss : MonoBehaviour , IDamagable
{  
  public float maxHealth;
  public float currentHealth;
  public SpriteRenderer spriteRenderer;
  [HideInInspector] public StateMachine stateMachine;
  public GameObject tentaclePrefab;
  public GameObject ballPreafab;
  public Animator animator;
  public int randomIndex ;
  public bool hitDone;
  void Start()
  {
    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    currentHealth = maxHealth;
    hitDone = false;
    randomIndex  = -1;
    animator = GetComponentInChildren<Animator>();
    LoadStateMachine();
  }
  void LoadStateMachine()
  {
    
  stateMachine = new StateMachine();

  IState idle = new BossIdle(this);
  IState Hit = new BossHit(this);
  IState Hit1 = new BossHit1(this);
  IState Hit2 = new BossHit2(this);     

  stateMachine.AddTransition(idle, Hit, isHit());
  stateMachine.AddTransition(Hit,idle ,HitDone());
  
  stateMachine.AddTransition(idle, Hit1, isHit1());
  stateMachine.AddTransition(Hit1, idle ,HitDone());

  stateMachine.AddTransition(idle, Hit2, isHit2());
  stateMachine.AddTransition(Hit2, idle ,HitDone());

  Func<bool> isHit()=> ()=> randomIndex == 0;
  Func<bool> isHit1()=> ()=> randomIndex == 1;
  Func<bool> isHit2()=> ()=> randomIndex == 2;
  Func<bool> HitDone() => () => hitDone;

  stateMachine.SetState(idle);
  }

  public void randomHit()
  {
    randomIndex = UnityEngine.Random.Range(0,3);
    //randomIndex = 0;
  }

  void Update()
  {
    stateMachine.Tick();
  }

  public void SpawnTentacle(Vector2 pos){
    Instantiate(tentaclePrefab,pos,quaternion.identity);
  }

   public void SpawnBall(Vector2 pos){
    Instantiate(ballPreafab,pos,quaternion.identity);
  }

  IEnumerator hitEffect(){
    spriteRenderer.color = Color.red;
    yield return new WaitForSeconds(0.1f);
    spriteRenderer.color = Color.white;
  }

  public void ReciveHit(float damageAmount)
  {
    currentHealth -= damageAmount;

    StartCoroutine(hitEffect());

    if(currentHealth <= 0){
      Destroy(gameObject);
    }
  }
}
