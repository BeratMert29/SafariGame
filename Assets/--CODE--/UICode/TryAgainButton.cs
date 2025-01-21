using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour
{

    [SerializeField] private GameObject panel, player;

    void Update()
    {
        if (player.GetComponent<Player>().currentHealth <= 0)
            panel.SetActive(true);
        else
            return;
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        panel.SetActive(false);
    }

}
