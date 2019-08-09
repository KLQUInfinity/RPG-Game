﻿using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int MaxHealth = 100;

    public int CurrentHealth{ get; private set; }

    public Stat Damage;
    public Stat Armour;

    void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        damage -= Armour.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //die in some way
    }
}
