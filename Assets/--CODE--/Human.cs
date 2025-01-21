using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Human : Character
{
    public float hit1CooldownTime = 1f; // hit1 için bekleme süresi
    public float hit2CooldownTime = 1.5f; // hit2 için bekleme süresi

    // Cooldown sayaçlarý
    private float hit1Cooldown = 0f;
    private float hit2Cooldown = 0f;

    public GameObject hitbox; // Saldýrý hitbox'ý

    void Update()
    {
        Move();
        Jump();
        Flip();

        animator.SetBool("run", moveX != 0);

        if (hit1Cooldown > 0)
        {
            hit1Cooldown -= Time.deltaTime;
        }

        if (hit2Cooldown > 0)
        {
            hit2Cooldown -= Time.deltaTime;
        }

        // Sol týklama (hit1)
        if (Input.GetMouseButtonDown(0) && hit1Cooldown <= 0)
        {
            animator.SetTrigger("hit1");
            hit1Cooldown = hit1CooldownTime; // Cooldown'u sýfýrla
            ActivateHitbox(); // Hitbox'ý aktif et
        }

        // Sað týklama (hit2)
        if (Input.GetMouseButtonDown(1) && hit2Cooldown <= 0)
        {
            animator.SetTrigger("hit2");
            hit2Cooldown = hit2CooldownTime; // Cooldown'u sýfýrla
            ActivateHitbox(); // Hitbox'ý aktif et
        }
    }

    private void ActivateHitbox()
    {
        hitbox.SetActive(true); // Hitbox'ý aktif et
        StartCoroutine(DeactivateHitboxAfterDelay(0.2f)); // Kýsa bir süre sonra kapat
    }

    private IEnumerator DeactivateHitboxAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hitbox.SetActive(false);
    }

    
}
