using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plusTrap : MonoBehaviour
{
    public GameObject bullet; // Mermi prefab'ý
    public float fireInterval = 1f; // Ateþleme aralýðý

    private bool isPlusMode = true; // Baþlangýçta + þeklinde ateþle

    private void Start()
    {
        StartCoroutine(FireBullets()); // Coroutine baþlat
    }

    private IEnumerator FireBullets()
    {
        while (true)
        {
            if (isPlusMode)
            {
                // + þeklinde ateþle
                SpawnBullet(Vector2.right);  // Sað
                SpawnBullet(Vector2.left);   // Sol
                SpawnBullet(Vector2.up);     // Yukarý
                SpawnBullet(Vector2.down);   // Aþaðý
            }
            else
            {
                // x þeklinde ateþle (çapraz)
                SpawnBullet(new Vector2(1, 1).normalized);   // Sað üst
                SpawnBullet(new Vector2(-1, 1).normalized);  // Sol üst
                SpawnBullet(new Vector2(1, -1).normalized);  // Sað alt
                SpawnBullet(new Vector2(-1, -1).normalized); // Sol alt
            }

            // Modu deðiþtir
            isPlusMode = !isPlusMode;

            yield return new WaitForSeconds(fireInterval); // Belirli bir süre bekle
        }
    }

    private void SpawnBullet(Vector2 direction)
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity); // Yeni mermiyi oluþtur
        plusTrapBullet bulletScript = newBullet.GetComponent<plusTrapBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction); // Mermiye yön ver
        }
    }
}
