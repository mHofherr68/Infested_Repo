using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This code is adapted from the book "Unity UI Cookbook" from Francesco Sapio,
// Packt Publishing Ltd, 2015, pages 46-51
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthbarFilling;
    public float maxHealth = 100f;
    public float currentHealth;
    public float damage = 10f;

    void Start() 
    {
        healthbarFilling = GameObject.Find("HealthBarCircular/Fill").GetComponent<Image>();
        currentHealth = maxHealth;
    }

    public void AddHealth(float heal) {
        currentHealth += heal;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        UpdateHealth();
    }

    public bool RemoveHealth(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Game Over");
            UpdateHealth();
            return true;
            
        }
        UpdateHealth();
        return false;
    }

    private void UpdateHealth()
    {
        healthbarFilling.fillAmount = (currentHealth / maxHealth);
    }
  
}
