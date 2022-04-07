using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject prefab;
    public int value;
    public float weight;
    public bool stackable;
    public string description;
}

[System.Serializable]
public class ItemInstance {
    // Reference to scriptable object "template".
    public Item item;
    public int quantity = 1;
    // Object-specific data.

    public ItemInstance(Item item, int quantity) {
        this.item = item;
        this.quantity = quantity;
    }

    public void AddQuantity(int amount)
    {
        quantity += amount;
    }

    public string GetItemName()
    {
        return item.itemName;
    }

    public Sprite GetItemIcon()
    {
        return item.icon;
    }
    public string GetItemDescription()
    {
        return item.description;
    }

    public float GetWeight()
    {
        return item.weight;
    }
    public float GetValue()
    {
        return item.value;
    }
}