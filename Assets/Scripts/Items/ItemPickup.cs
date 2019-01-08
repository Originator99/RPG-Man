using UnityEngine;

public class ItemPickup : IntractableO {

    public Item item;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    private void PickUp() {
        Debug.Log("Picking up " + item.name);
        //Adding to inventory
        if(Inventory.instance.Add(item))
            Destroy(gameObject); // Destory after adding to Inventory. 
    }

}
