using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Items/Potion", fileName = "NewPotion.asset")]
public class Potion : Item
{
    public enum PotionType {
        Health, Magic, Stamina
    }
    public PotionType potionType;
    public int modifier;

}
