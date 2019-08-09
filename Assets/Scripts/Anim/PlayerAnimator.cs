using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    public WeaponAnimations[] WeaponAnims;

    private Dictionary<Equipment,AnimationClip[]> weaponAnimsDictionary;

    protected override void Start()
    {
        base.Start();
        EquipmentManager.Instance.OnEquipmentChangedCallback += OnEquipmentChanged;

        weaponAnimsDictionary = new Dictionary<Equipment, AnimationClip[]>();
        foreach (WeaponAnimations i in WeaponAnims)
        {
            weaponAnimsDictionary.Add(i.Weapon, i.Clips);
        }
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null && newItem.EquipSlot == EquipmentSlot.Weapon)
        {
            myAnim.SetLayerWeight(1, 1);
            if (weaponAnimsDictionary.ContainsKey(newItem))
            {
                currentAttackAnimSet = weaponAnimsDictionary[newItem];  
            }
        }
        else if (newItem == null && oldItem != null && oldItem.EquipSlot == EquipmentSlot.Weapon)
        {
            myAnim.SetLayerWeight(1, 0);
            currentAttackAnimSet = DefaultAttackAnimSet;
        }

        if (newItem != null && newItem.EquipSlot == EquipmentSlot.Shield)
        {
            myAnim.SetLayerWeight(2, 1);
        }
        else if (newItem == null && oldItem != null && oldItem.EquipSlot == EquipmentSlot.Shield)
        {
            myAnim.SetLayerWeight(2, 0);
        }
    }

    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment Weapon;
        public AnimationClip[] Clips;
    }
}
