using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

	// Use this for initialization
	void Start () {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
		
	}

    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }
        if (oldItem != null)
        {
            armor.RemoveModifer(oldItem.armorModifier);
            damage.RemoveModifer(oldItem.damageModifier);
        }
    }

    public override void Die()
    {
        base.Die();
        //Kill Player animation
        PlayerManager.instance.KillPlayer();
    }
}
