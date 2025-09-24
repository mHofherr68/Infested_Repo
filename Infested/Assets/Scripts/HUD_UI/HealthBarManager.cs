using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This code is adapted from the book "Unity UI Cookbook" from Francesco Sapio,
// Packt Publishing Ltd, 2015, pages 46-51
public class HealthBarManager : MonoBehaviour
{
    // Reference to the UI Image component representing the health bar filling
    [SerializeField] private Image healthbarFilling;
    // Maximum health value
    public float maxHealth = 100f;
    // Current health value
    public float currentHealth;
    // Damage value from enemy collision
    public float damage = 10f;

    void Start() 
    {   // Initialize health bar filling and current health
        healthbarFilling = GameObject.Find("HealthBarCircular/Fill").GetComponent<Image>();
        currentHealth = maxHealth;
    }

    // Method to add health to the player
    public void AddHealth(float heal) {
        currentHealth += heal;
        // Ensure current health does not exceed maximum health
        if (currentHealth> maxHealth)
            currentHealth = maxHealth;
        // Update the health bar UI
        UpdateHealth();
    }

    // Method to remove health from the player
    public bool RemoveHealth(float damage)
    {
        currentHealth -= damage;
        // Check if current health has dropped to zero or below
        if (currentHealth <= 0)
        {
            // Set current health to zero and log "Game Over"
            currentHealth = 0;
            // Update the health bar UI
            UpdateHealth();
            // Return true to indicate the player has died
            return true;
            
        }
        UpdateHealth();
        // Return false to indicate the player is still alive
        return false;
    }

    // Method to update the health bar UI based on current health
    private void UpdateHealth()
    {
        // Update the fill amount of the health bar image; works for normal bar and circular bar
        healthbarFilling.fillAmount = (currentHealth / maxHealth);
    }
  
}
