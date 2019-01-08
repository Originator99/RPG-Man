using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Equipment", menuName ="Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;
    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        //Equip Item
        EquipmentManager.instance.Equip(this);
        //Remove Item from Inventory
        RemovefromInventory();
    }

}

public enum EquipmentSlot {
HEAD,
CHEST,
LEGS,
WEAPON,
SHIELD,
FEET
}

public enum EquipmentMeshRegion {
LEGS,
ARMS,
TORSO
} //Corresponds to body blendshapes
