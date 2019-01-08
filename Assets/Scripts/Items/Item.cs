using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    //This will act as a blueprint for the item

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefault = false;

    public virtual void Use() {
        //Using the item

        Debug.Log("Using  :" + name);
    }

    public void RemovefromInventory() {
        Inventory.instance.Remove(this);
    }
}
