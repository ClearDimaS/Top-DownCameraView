
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    Image healthBar;
    private float currentHealth;
    private float maxHealth;

    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    public void UpdateHealthView(int currentHealth, int maxHealth)
    {
        healthBar.fillAmount = (currentHealth / (float)maxHealth);
    }
}
