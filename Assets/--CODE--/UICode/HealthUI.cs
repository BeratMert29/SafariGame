using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    private float healthBarHeight = 150.023f, healthBarWidth = 21.0991f;
    private int startingHealth = 10;
    public float yOffset = 2f;
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = healthBar.GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(0.5f, 0f);
    }

    void Update()
    {
        HealthBarManagement();
    }

    private void HealthBarManagement()
    {
        if (Player.instance.currentHealth < startingHealth)
        {
            startingHealth = (int)Player.instance.currentHealth;
            rectTransform.sizeDelta = new Vector2(healthBarWidth, healthBarHeight - 15);
            healthBarHeight -= 15;
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + yOffset);
        }
    }

}