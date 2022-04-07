using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : InteractComponent
{
    public ItemInstance item;

    public override void OnInteract(PlayerStats playerStats)
    {
        playerStats.inventory.InsertItem(item);
        Destroy(gameObject);
    }

}
