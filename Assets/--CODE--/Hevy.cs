using System.Collections;
using UnityEngine;

public class Hevy : MonoBehaviour
{
    [SerializeField] private GameObject hitbox; // Hitbox objesini buraya baðla
     private Animator animator; // Animator objesini buraya baðla
    private float cooldown = 3f; // Hitbox'ýn aktive olma süresi
    private float activeDuration = 0.5f; // Hitbox'ýn aktif kalma süresi

    private void Start()
    {
        // Coroutine baþlat
        StartCoroutine(HitboxCooldownRoutine());
        animator=GetComponent<Animator>();
    }

    private IEnumerator HitboxCooldownRoutine()
    {
        while (true)
        {
            // 5 saniyede bir çalýþacak
            yield return new WaitForSeconds(cooldown);

            // Hitbox'ý aktif et
            hitbox.SetActive(true);

            // Animasyon tetikleyicisini çaðýr
            animator.SetTrigger("hit");

            // Hitbox 0,5 saniye açýk kalacak
            yield return new WaitForSeconds(activeDuration);

            // Hitbox'ý devre dýþý býrak
            hitbox.SetActive(false);
        }
    }
}
