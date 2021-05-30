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
    public int MaxAmmo = 7;
    public int Magazines = int.MaxValue;

    public float Speed = 2;
    public float SprintSpeed = 2.5f;
    
    public Text ReserveAmmoText;
    public Text CurrentAmmoText;

    public float ShootingDelay = 1;
    public float ReloadDelay = 5;
    
    void Start()
    {
        CurrentHealth = MaxHealth;
        
        CurrentStamina = MaxHealth;
        MaxStamina = MaxHealth;

        Magazines = int.MaxValue;
        
        CurrentAmmo = MaxAmmo;
        
        HealthBar.SetMaxHealth(MaxHealth);
    }

    void Update()
    {
        // stamina stuff
        MaxStamina = CurrentHealth;
        CurrentStamina = Math.Min(MaxStamina, CurrentStamina);  // restrict stamina to health
        
        // UI
        HealthBar.SetHealth(CurrentHealth);
        StaminaBar.SetStamina(CurrentStamina);
        
        CurrentAmmoText.text = CurrentAmmo.ToString();
        ReserveAmmoText.text = "∞";
    }
}
