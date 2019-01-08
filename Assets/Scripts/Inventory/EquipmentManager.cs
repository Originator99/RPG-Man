using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton

    public static EquipmentManager instance;
    private void Awake()
    {
        instance = this;
    }

    #endregion

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    public Equipment[] defaultItems;
    public SkinnedMeshRenderer targetMesh;

    Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;
    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }

    public void Equip(Equipment newItem) {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = Unequip(slotIndex);
        
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(newItem, oldItem);
        currentEquipment[slotIndex] = newItem;

        SetEquipmentBlendShape(newItem, 100);

        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }

    public Equipment Unequip(int slotID)
    {
        if (currentEquipment[slotID] != null)
        {
            if (currentMeshes[slotID] != null) {
                Destroy(currentMeshes[slotID].gameObject);
            }
            Equipment oldItem = currentEquipment[slotID];
            SetEquipmentBlendShape(oldItem, 0);
            inventory.Add(oldItem);
            currentEquipment[slotID] = null;
            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, oldItem);

            return oldItem;
        }
        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
            Unequip(i);
        EquipDefaultItems();
    }

    private void SetEquipmentBlendShape(Equipment item, int weight)
    {
        foreach(EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    private void EquipDefaultItems() {
        foreach(Equipment item in defaultItems)
        {
            Equip(item);
        }
    }
}
