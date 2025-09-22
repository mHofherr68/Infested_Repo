using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class UIPlayerData : MonoBehaviour
{
    //Maximum health of the player
    public int maxHealth = 100;
    //Current health of the player
    public int currentHealth;

    // Reference to the PlayerInput component
    private PlayerInput playerInput;

    // Reference to HealthBar script
    public HealthBar healthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set current health to max health at the start
        currentHealth = maxHealth;
        //healthBar = GameObject.Find("Player/PlayerCam/UICanvas/HealthBar").GetComponent<HealthBar>();
        playerInput = GetComponent<PlayerInput>();
        // Then call SetMaxHealth on the health bar
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onDamage(CallbackContext ctx) {
        // Simulate taking damage when the action is performed
        if (currentHealth != 0) {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
