using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{
    [SerializeField] private GameObject levelFinisherObject;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && SceneManager.GetActiveScene().name == "Level-1")
        {
            SceneManager.LoadScene(4);
        }

        else if (collision.gameObject.name == "Player" && SceneManager.GetActiveScene().name == "Level-2")
        {
            SceneManager.LoadScene(5);
        }

    }

}
