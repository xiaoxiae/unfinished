using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float MaxHealth = 100;
    public float CurrentHealth;
    public HealthBar HealthBar;
    
    public float MaxStamina;
    public float CurrentStamina;
    public StaminaBar StaminaBar;

    public int CurrentAmmo = 7;
    public int MaxWeaponAmmo = 7;
    public int ReserveAmmo = 0;

    public float Speed = 2;
    public float SprintSpeed = 2.5f;
    
    public Text ReserveAmmoText;
    public Text CurrentAmmoText;

    public int ShootingDelay = 1;
    public int ReloadDelay = 5;
    
    void Start()
    {
        CurrentHealth = MaxHealth;
        
        CurrentStamina = MaxHealth;
        MaxStamina = MaxHealth;
        
        CurrentAmmo = MaxWeaponAmmo;
        ReserveAmmo = 0;
        
        HealthBar.SetMaxHealth(MaxHealth);
    }

    public int Ammo = 7;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            CurrentHealth -= 10;
        
        // stamina stuff
        MaxStamina = MaxHealth;
        CurrentStamina = Math.Min(MaxStamina, CurrentStamina);  // restrict stamina to health
        
        // UI
        HealthBar.SetHealth(CurrentHealth);
        StaminaBar.SetStamina(CurrentStamina);
        
        CurrentAmmoText.text = CurrentAmmo.ToString();
        ReserveAmmoText.text = ReserveAmmo.ToString();
    }
}
