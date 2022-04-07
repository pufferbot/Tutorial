using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory", fileName = "NewInventory.asset")]
[System.Serializable]
public class Inventory : ScriptableObject
{
    // Inventory Code

    public ItemInstance[] inventory;

    public int gold;

    public bool SlotEmpty(int index) {
        if (inventory[index] == null || inventory[index].item == null)
            return true; //triggers if there is no item instance or if item instance has no item

        return false;
    }
    
    // Get an item if it exists.
    public bool GetItem(int index, out ItemInstance item) {
        // inventory[index] doesn't return null, so check item instead.
        if (SlotEmpty(index)) {
            item = null;
            return false;
        }

        item = inventory[index];
        return true;
    }

    // Remove an item at an index if one exists at that index.
    public bool RemoveItem(int index) {
        if (SlotEmpty(index)) {
            // Nothing existed at the specified slot.
            return false;
        }

        inventory[index] = null;

        return true;
    }

    // Insert an item, return the index where it was inserted
    public int InsertItem(ItemInstance item) {
        for (int i = 0; i < inventory.Length; i++) {
            if (SlotEmpty(i)) {
                inventory[i] = item;
                return i;
            }
        }

        // Couldn't find a free slot, so make a new one and insert the item into new slot
        int position = inventory.Length;
        ItemInstance[] temp = new ItemInstance[position+1];
        inventory.CopyTo(temp, 0);
        inventory = temp;
        inventory[position] = item;
        return position;
    }

    public float TotalWeight()
    {
        float weight = 0;
        for(int i = 0; i < inventory.Length; i++)
        {
            weight += inventory[i].GetWeight();
        }
        return weight;
    }

}
