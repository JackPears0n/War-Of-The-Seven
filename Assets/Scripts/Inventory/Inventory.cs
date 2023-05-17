using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple instances of inventory found");
            return;
        }
        instance = this;
    }
    #endregion
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        if (!item.isDefaultItem)
        {
            items.Add(item);
        }
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }
}
