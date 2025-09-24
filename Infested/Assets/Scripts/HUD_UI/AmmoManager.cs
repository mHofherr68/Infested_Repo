using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

// This code is adapted from the book "Unity UI Cookbook" from Francesco Sapio,
// Packt Publishing Ltd, 2015, pages 28-29
public class AmmoManager : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoDisplayText;
    private int currentAmmo; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ammoDisplayText = GameObject.Find("AmmoDisplay/AmmoDisplayText").GetComponent<TMP_Text>();
        UpdateAmmoCounter(); 
    }

    public void AddAmmo(int ammo)
    {
        currentAmmo += ammo;
        UpdateAmmoCounter();

    }

    public void ReduceAmmo(int ammo)
    {
        currentAmmo -= ammo;
        UpdateAmmoCounter();
    }

    private void UpdateAmmoCounter()
    {
        ammoDisplayText.text = currentAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
