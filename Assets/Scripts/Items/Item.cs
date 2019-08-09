using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    new public string name = "New Item";
    public Sprite Icon = null;
    public bool IsDefaultItem = false;

    public virtual void Use()
    {
        //use the item
        //something maight happen
    }

    public void RemoveFromInventory()
    {
        Inventory.Instance.Remove(this);
    }

}
