using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image Icon;
    public Button RemoveBtn;

    private Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        Icon.sprite = item.Icon;
        Icon.enabled = true;
        RemoveBtn.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        Icon.sprite = null;
        Icon.enabled = false;
        RemoveBtn.interactable = false;
    }

    public void OnRemoveBtn()
    {
        Inventory.Instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
