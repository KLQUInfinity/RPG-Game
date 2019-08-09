using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] private Item Item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        if (Inventory.Instance.Add(Item))
        {
            Destroy(gameObject);
        }
    }
}
