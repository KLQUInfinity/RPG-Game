using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    void Start()
    {
        EquipmentManager.Instance.OnEquipmentChangedCallback += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldTown)
    {
        if (newItem != null)
        {
            Armour.AddModifier(newItem.ArmorModifier);
            Damage.AddModifier(newItem.DamageModifier);
        }

        if (oldTown != null)
        {
            Armour.RemoveModifier(oldTown.ArmorModifier);
            Damage.RemoveModifier(oldTown.DamageModifier);
        }
    }

    public override void Die()
    {
        base.Die();
        //kill the plater
        PlayerManager.Instance.KillPlayer();
    }
}
