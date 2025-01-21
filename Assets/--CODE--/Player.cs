using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    public static Player instance;
    //stats
    public float maxHealth;
    public float currentHealth;

    [SerializeField] GameObject[] animalModes;
    [SerializeField] Transform modeContainer;
    public List<GameObject> createdModeList = new List<GameObject>();
    private int modeIndex;

    public bool isDead = false;
    [SerializeField] private GameObject tryAgainPanel;


    void Awake()
    {
        instance = this;
        currentHealth = maxHealth;

        foreach (GameObject animal in animalModes)
        {
            GameObject a = Instantiate(animal, modeContainer);
            a.GetComponent<Character>().Initilize();
            a.SetActive(false);
            createdModeList.Add(a);
        }

        modeIndex = 0;
        createdModeList[modeIndex].SetActive(true);

    }

    void FixedUpdate()
    {
        if (gameObject.transform.position.y < -20)
        {
            Die();
            tryAgainPanel.SetActive(true);
        }
    }

    public void StartCoolDownCouroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public void ReciveHit(float damageAmount)
    {
        //currentHealth -= damageAmount * createdModeList[modeIndex].GetComponent<Character>().damageReduction;
        currentHealth -= damageAmount;
        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player is Dead!");
        gameObject.SetActive(false);
    }

    public GameObject[] GetAnimalModes()
    {
        return animalModes;
    }
}
