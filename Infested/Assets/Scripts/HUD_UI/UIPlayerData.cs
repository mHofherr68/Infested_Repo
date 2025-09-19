using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerData : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    //private BaseCharacterController baseCharacterController;
    public HealthBar healthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        // GameObject.Find("Player").GetComponent<BaseCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage) 
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
