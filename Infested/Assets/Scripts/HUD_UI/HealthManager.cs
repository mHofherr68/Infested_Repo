using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    // ----- Display and Updating Health Bar ----- //

    // Reference to the Image component that represents the radial fill
    [SerializeField] private RadialFillManager radialFillManager;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;


    // ----- Damage Handling -----//
    public int damage = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        radialFillManager = GameObject.Find("Player/PlayerCam/UICanvas/HealthBar").GetComponent<RadialFillManager>();
        currentHealth = maxHealth;
        radialFillManager.UpdateRadialProgressCircle(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth >= 0)
        {
            TakeDamage(damage);
        }

        // Radial Health Bar aktualisieren
        radialFillManager.UpdateRadialProgressCircle(currentHealth, maxHealth);
    
        if (currentHealth <= 0) 
        {
            Debug.Log("Game Over");
        }
    }

    
}
