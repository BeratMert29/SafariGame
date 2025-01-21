using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public float hitPoint;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Collider2D collider;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }
    void Start()
    {
        collider.enabled = false;
        spriteRenderer.sortingOrder = -(int)math.ceil(transform.position.y * 30);
        StartCoroutine(TentacleLife());
    }

    void Update()
    {

    }
    IEnumerator TentacleLife()
    {
        int r = UnityEngine.Random.Range(0, 2);
        if (r == 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        animator.Play("OutTenta");

        yield return new WaitForSeconds(0.1f);
        collider.enabled = true;
        animator.Play("IdleTenta");

        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null)
        {
            player.ReciveHit(hitPoint);
        }
    }
}
