using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot EquipSlot;
    public SkinnedMeshRenderer Mesh;

    public int ArmorModifier;
    public int DamageModifier;

    public EquipmentMeshRegion[] CoveredMeshRegions;

    public override void Use()
    {
        base.Use();
        //equip the item
        EquipmentManager.Instance.Equip(this);

        //remove it from the inventory
        RemoveFromInventory();
    }
}

public enum EquipmentSlot
{
    Head,
    Chest,
    Legs,
    Weapon,
    Shield,
    Feet
}

public enum EquipmentMeshRegion //correspons to body blendshapes
{
    Legs,
    Arms,
    Torso
}
