using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] int baseValue;
    private List<int> modifiers = new List<int>();

    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public int GetBaseValue()
    {
        return baseValue;
    }

    public void SetBaseValue(int newValue)
    {
        baseValue = newValue;
    }

    public void AttributeBonus(int attribute, int multiplier = 2, int additioner=0) //additioner
    {
        baseValue = (attribute + 5) * multiplier + additioner; //This makes it so if you have the default 5 in a skill it ends up with 10, which I think given skills are 1-100 is good
    }

    public void AddModifier(int modifier) //This function is for adding a temporary modifier to a stat
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }
}
