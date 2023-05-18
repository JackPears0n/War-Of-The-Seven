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

    public delegate void OnItemChange();
    public OnItemChange onItemChangedCallback;

    public int space = 25;


    // Makess a list for the items
    public List<Item> items = new List<Item>();

    // Adds and item
    public bool AddItem(Item item)
    {
        if (!item.isDefaultItem)
        {
            // If there's not enough room, item remains untouched
            if (items.Count >= space)
            {
                print("Not enough room");
                return false;
            }

            // Puts item in list if there's room
            items.Add(item);

            // Triggers event to change UI
            if(onItemChangedCallback !=null)
            {
                onItemChangedCallback.Invoke(); 
            }
        }

        return true;
    }

    public void RemoveItem(Item item)
    {
        // Removes an item
        items.Remove(item);

        // Triggers event to change UI
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
