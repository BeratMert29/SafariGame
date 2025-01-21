using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plusTrap : MonoBehaviour
{
    public GameObject bullet; // Mermi prefab'�
    public float fireInterval = 1f; // Ate�leme aral���

    private bool isPlusMode = true; // Ba�lang��ta + �eklinde ate�le

    private void Start()
    {
        StartCoroutine(FireBullets()); // Coroutine ba�lat
    }

    private IEnumerator FireBullets()
    {
        while (true)
        {
            if (isPlusMode)
            {
                // + �eklinde ate�le
                SpawnBullet(Vector2.right);  // Sa�
                SpawnBullet(Vector2.left);   // Sol
                SpawnBullet(Vector2.up);     // Yukar�
                SpawnBullet(Vector2.down);   // A�a��
            }
            else
            {
                // x �eklinde ate�le (�apraz)
                SpawnBullet(new Vector2(1, 1).normalized);   // Sa� �st
                SpawnBullet(new Vector2(-1, 1).normalized);  // Sol �st
                SpawnBullet(new Vector2(1, -1).normalized);  // Sa� alt
                SpawnBullet(new Vector2(-1, -1).normalized); // Sol alt
            }

            // Modu de�i�tir
            isPlusMode = !isPlusMode;

            yield return new WaitForSeconds(fireInterval); // Belirli bir s�re bekle
        }
    }

    private void SpawnBullet(Vector2 direction)
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity); // Yeni mermiyi olu�tur
        plusTrapBullet bulletScript = newBullet.GetComponent<plusTrapBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction); // Mermiye y�n ver
        }
    }
}
