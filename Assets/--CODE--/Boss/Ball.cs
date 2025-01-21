using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float duration;
    private Vector3 target;
    private float startTime;
    void Start()
    {
        if(Player.instance != null){
            target = (Player.instance.transform.position - transform.position).normalized;
        }
        startTime = Time.time;
    }

    void Update()
    {
        transform.Translate(target * Time.deltaTime * speed);

        if(Time.time - startTime >= duration){
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col){
        Player player = col.GetComponent<Player>();
        if(player != null){
            player.ReciveHit(3);
        }
    }
}
