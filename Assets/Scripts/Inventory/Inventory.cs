using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Instance of Inventory");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public List<Item> items = new List<Item>();

    public int space = 20;

    public bool Add(Item item) {
        if (!item.isDefault)
        {
            if (items.Count >= space) {
                Debug.Log("Not enough space in Inventory");
                return false;
            }
            items.Add(item);
            if (OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item item) {
        items.Remove(item);
        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }

}
