using System.Collections;
using UnityEngine;

public class Hevy : MonoBehaviour
{
    [SerializeField] private GameObject hitbox; // Hitbox objesini buraya ba�la
     private Animator animator; // Animator objesini buraya ba�la
    private float cooldown = 3f; // Hitbox'�n aktive olma s�resi
    private float activeDuration = 0.5f; // Hitbox'�n aktif kalma s�resi

    private void Start()
    {
        // Coroutine ba�lat
        StartCoroutine(HitboxCooldownRoutine());
        animator=GetComponent<Animator>();
    }

    private IEnumerator HitboxCooldownRoutine()
    {
        while (true)
        {
            // 5 saniyede bir �al��acak
            yield return new WaitForSeconds(cooldown);

            // Hitbox'� aktif et
            hitbox.SetActive(true);

            // Animasyon tetikleyicisini �a��r
            animator.SetTrigger("hit");

            // Hitbox 0,5 saniye a��k kalacak
            yield return new WaitForSeconds(activeDuration);

            // Hitbox'� devre d��� b�rak
            hitbox.SetActive(false);
        }
    }
}
