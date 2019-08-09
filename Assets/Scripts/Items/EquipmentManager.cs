using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    #region Singleton

    public static EquipmentManager Instance;

    void Awake()
    {
        Instance = this;
    }

    #endregion

    public delegate void OnEquipmentChanged(Equipment newItem,Equipment oldItem);

    public OnEquipmentChanged OnEquipmentChangedCallback;

    public Equipment[] DefaultItems;
    public SkinnedMeshRenderer TargetMesh;

    private Equipment[] currentEquipment;
    private SkinnedMeshRenderer[] currentMeshes;

    void Start()
    {
        currentEquipment = new Equipment[System.Enum.GetNames(typeof(EquipmentSlot)).Length];
        currentMeshes = new SkinnedMeshRenderer[System.Enum.GetNames(typeof(EquipmentSlot)).Length];

        EquipDefaultItems();
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.EquipSlot;
        Equipment oldItem = UnEquip(slotIndex);

        if (OnEquipmentChangedCallback != null)
        {
            OnEquipmentChangedCallback.Invoke(newItem, oldItem);
        }

        SetEquipmentBlendShapes(newItem, 100);

        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.Mesh);
        newMesh.transform.SetParent(TargetMesh.transform);

        newMesh.bones = TargetMesh.bones;
        newMesh.rootBone = TargetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }

    public Equipment UnEquip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem, 0);
            Inventory.Instance.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (OnEquipmentChangedCallback != null)
            {
                OnEquipmentChangedCallback.Invoke(null, oldItem);
            }
            return oldItem;
        }
        return null;
    }

    public void UnEquipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            UnEquip(i);
        }
        EquipDefaultItems();
    }

    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.CoveredMeshRegions)
        {
            TargetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems()
    {
        foreach (Equipment i in DefaultItems)
        {
            Equip(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnEquipAll();
        }
    }
}
