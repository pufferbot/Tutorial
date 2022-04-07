using System.Collections;
using UnityEngine;

public class CharacterStats : MonoBehaviour, ISavable
{

    //Basically, this script in of itself shouldn't actually be going onto anything, instead there should be seperate scripts that derive from CharacterStats 
    //instead of MonoBehavior for each type of character. For example, Goblin stats would be this: public class GoblinStats : CharacterStats

    public GameManager gameManager;
    
    public Inventory inventory;
    public Item equippedItem;
    
    public GameObject itemHolder;
    public GameObject heldItem;

    public int basicStatAdditioner = 50;
    public int basicStatMultiplier = 5;

    public Vector3 characterPosition;
    public Vector3 characterRotation;

    public int level;

    //The three main resource bars
    public Stat maxHealth;
    public float currentHealth;
    public Stat maxMagic;
    public float currentMagic;
    public Stat maxStamina;
    public float currentStamina;

    //stuff related to regeneration
    float lastLostHealth = 0f;
    public float healthRegenTimer = 5f;
    bool isRegenHealth = false;
    float lastLostMagic = 0f;
    public float magicRegenTimer = 2f;
    bool isRegenMagic = false;
    float lastLostStamina = 0f;
    public float staminaRegenTimer = 1.5f;
    bool isRegenStamina = false;

    public Stat damageResistance;
    public Stat carryCapacity;
    public bool overEncumbered = false;

    //The main attribute stats
    public Stat strength;
    public Stat dexterity;
    public Stat constitution;
    public Stat intelligence;
    public Stat charisma;

    //Skills. Naming convention is skill_[Name], so they can be found easier later on. Seperated based on the attribute that controls it

    //Strength
    public Stat skill_Melee;
    public Stat skill_Unarmed;
    //Dexterity
    public Stat skill_Accuracy;
    public Stat skill_Ranged;
    public Stat skill_SleightOfHand;
    public Stat skill_Stealth;
    //Constitution
    public Stat skill_Defense;
    public Stat skill_Willpower;
    //Intelligence
    public Stat skill_Crafting;
    public Stat skill_Observation;
    public Stat skill_Magic;
    public Stat skill_Medicine;
    //Charisma
    public Stat skill_Barter;
    public Stat skill_Deception;
    public Stat skill_Intimidation;
    public Stat skill_Persuasion;

    int[] statsList;
    int[] skillsList;

    public void SetStats()
    {
        //Here we should set the non-skill stats
        currentHealth = maxHealth.GetValue();
        currentMagic = maxMagic.GetValue();
        currentStamina = maxStamina.GetValue();

        carryCapacity.AttributeBonus(strength.GetValue(), basicStatMultiplier, basicStatAdditioner); //50-100 base carry capacity
    }

    public void SetInitialSkills() //exactly what it says lol
    {
        //Strength
        skill_Melee.AttributeBonus(strength.GetValue());
        skill_Unarmed.AttributeBonus(strength.GetValue());
        //Dexterity
        skill_Accuracy.AttributeBonus(dexterity.GetValue());
        skill_Ranged.AttributeBonus(dexterity.GetValue());
        skill_SleightOfHand.AttributeBonus(dexterity.GetValue());
        skill_Stealth.AttributeBonus(dexterity.GetValue());
        //Constitution
        skill_Defense.AttributeBonus(constitution.GetValue());
        skill_Willpower.AttributeBonus(constitution.GetValue());
        //Intelligence
        skill_Crafting.AttributeBonus(intelligence.GetValue());
        skill_Observation.AttributeBonus(intelligence.GetValue());
        skill_Magic.AttributeBonus(intelligence.GetValue());
        skill_Medicine.AttributeBonus(intelligence.GetValue());
        //Charisma
        skill_Barter.AttributeBonus(charisma.GetValue());
        skill_Deception.AttributeBonus(charisma.GetValue());
        skill_Intimidation.AttributeBonus(charisma.GetValue());
        skill_Persuasion.AttributeBonus(charisma.GetValue());
    }

    IEnumerator AddModifier(Stat stat, int modifier, float timer = -1) //This is for modifying stats. If you don't put in the timer, it will not be applied
    {
        stat.AddModifier(modifier);
        if(timer > 0)
        {
            yield return new WaitForSeconds(timer);
            stat.RemoveModifier(modifier);
        }
        yield break;
    }

    public void SetSkillsList()
    {
        skillsList = new int[21];

        //Strength
        skillsList[0] = strength.GetBaseValue();
        skillsList[1] = skill_Melee.GetBaseValue();
        skillsList[2] = skill_Unarmed.GetBaseValue();
        //Dexterity
        skillsList[3] = dexterity.GetBaseValue();
        skillsList[4] = skill_Accuracy.GetBaseValue();
        skillsList[5] = skill_Ranged.GetBaseValue();
        skillsList[6] = skill_SleightOfHand.GetBaseValue();
        skillsList[7] = skill_Stealth.GetBaseValue();
        //Constitution
        skillsList[8] = constitution.GetBaseValue();
        skillsList[9] = skill_Defense.GetBaseValue();
        skillsList[10] = skill_Willpower.GetBaseValue();
        //Intelligence
        skillsList[11] = intelligence.GetBaseValue();
        skillsList[12] = skill_Crafting.GetBaseValue();
        skillsList[13] = skill_Observation.GetBaseValue();
        skillsList[14] = skill_Magic.GetBaseValue();
        skillsList[15] = skill_Medicine.GetBaseValue();
        //Charisma
        skillsList[16] = charisma.GetBaseValue();
        skillsList[17] = skill_Barter.GetBaseValue();
        skillsList[18] = skill_Deception.GetBaseValue();
        skillsList[19] = skill_Intimidation.GetBaseValue();
        skillsList[20] = skill_Persuasion.GetBaseValue();
    }

    public void DamageHealth(int damage)
    {
        damage = Mathf.RoundToInt( Mathf.Max( 1.0f, damage * ((100 - Mathf.Min( damageResistance.GetValue(), 85.0f )) / 100) ));
        //This is like the worst line of code ever lol
        //Basically, it takes the damageResistance, capped at 85%, and subtracts that much from the incoming damage
        //Then, it takes either that value or 1.0, whichever is bigger
        //Then, if THAT number has a decimal, it rounds it down, finally giving us the total damage

        currentHealth -= damage;
        lastLostHealth = Time.time;
        Debug.Log(transform.name + " took " + damage + " damage");

        if(currentHealth >= 0)
        {
            RefreshStats();
        }
        else
        {
            Die();
        }
    }

    public void DamageStamina(float damage)
    {
        lastLostStamina = Time.time;
        if(currentStamina > 0)
        {
            currentStamina -= damage;
        }
        if (currentStamina < 0)
            currentStamina = 0;
    }
    public void DamageMagic(float damage)
    {
        lastLostMagic = Time.time;
        if (currentMagic > 0)
        {
            currentMagic -= damage;
        }
        if (currentMagic < 0)
            currentMagic = 0;
    }

    public IEnumerator RegenStats()
    {
        if (!isRegenHealth && Time.time > lastLostHealth + healthRegenTimer) //stops function from having multiple instances at the same time
        {
            isRegenHealth = true;
            while (currentHealth < maxHealth.GetValue())
            {
                currentHealth += (constitution.GetValue() + 6f)/10f;
                if (currentHealth > maxHealth.GetValue())
                    currentHealth = maxHealth.GetValue();
                yield return new WaitForSeconds (0.1f); //regains hp every second
            }
            isRegenHealth = false;
        }
        if (!isRegenMagic && Time.time > lastLostMagic + magicRegenTimer)
        {
            isRegenMagic = true;
            while (currentMagic < maxMagic.GetValue()) 
            {
                currentMagic += (intelligence.GetValue() + 6f)/10f;
                if (currentMagic > maxMagic.GetValue())
                    currentMagic = maxMagic.GetValue();
                yield return new WaitForSeconds (0.1f); //regains magic every second
            }
            isRegenMagic = false;
        }
        if (!isRegenStamina && Time.time > lastLostStamina + staminaRegenTimer)
        {
            isRegenStamina = true;
            while (currentStamina < maxStamina.GetValue()) 
            {
                currentStamina += (strength.GetValue() + 6f)/10f;
                if (currentStamina > maxStamina.GetValue())
                    currentStamina = maxStamina.GetValue();
                yield return new WaitForSeconds (0.1f); //regains stamina every second
            }
            isRegenStamina = false;
        }
    }

    public void EquipItem(ItemInstance itemToEquip)
    {
        //Remove any equipped items
        foreach (Transform child in itemHolder.transform)
            GameObject.Destroy(child.gameObject);
        equippedItem = null;

        //Equip the new item
        if(itemToEquip.item is MeleeWeapon meleeWeapon)
        {
            heldItem = Instantiate(meleeWeapon.heldPrefab, itemHolder.transform);
            equippedItem = itemToEquip.item;
        }
    }

    //Saving and Loading
    public object CaptureState()
    {
        SaveData _SaveData = new SaveData(new float[3], new float[3], new int[10], new int[16]);

        _SaveData.characterPosition[0] = characterPosition.x; _SaveData.characterPosition[1] = characterPosition.y; _SaveData.characterPosition[2] = characterPosition.z;
        _SaveData.characterRotation[0] = characterRotation.x; _SaveData.characterRotation[1] = characterRotation.y; _SaveData.characterRotation[2] = characterRotation.z;

        _SaveData.level = level;
        _SaveData.currentHealth = currentHealth;
        _SaveData.currentMagic = currentMagic;
        _SaveData.currentStamina = currentStamina;

        _SaveData.statsList[0] = maxHealth.GetBaseValue(); _SaveData.statsList[1] = maxMagic.GetBaseValue(); _SaveData.statsList[2] = maxStamina.GetBaseValue();
        _SaveData.statsList[3] = damageResistance.GetBaseValue(); _SaveData.statsList[4] = carryCapacity.GetBaseValue();
        _SaveData.statsList[5] = strength.GetBaseValue(); _SaveData.statsList[6] = intelligence.GetBaseValue(); _SaveData.statsList[7] = dexterity.GetBaseValue();
        _SaveData.statsList[8] = charisma.GetBaseValue(); _SaveData.statsList[9] = constitution.GetBaseValue();

        //Strength
        _SaveData.skillsList[0] =  skill_Melee.GetBaseValue();
        _SaveData.skillsList[1] =  skill_Unarmed.GetBaseValue();
        //Dexterity
        _SaveData.skillsList[2] =  skill_Accuracy.GetBaseValue();
        _SaveData.skillsList[3] =  skill_Ranged.GetBaseValue();
        _SaveData.skillsList[4] =  skill_SleightOfHand.GetBaseValue();
        _SaveData.skillsList[5] =  skill_Stealth.GetBaseValue();
        //Constitution
        _SaveData.skillsList[6] =  skill_Defense.GetBaseValue();
        _SaveData.skillsList[7] =  skill_Willpower.GetBaseValue();
        //Intelligence
        _SaveData.skillsList[8] =  skill_Crafting.GetBaseValue();
        _SaveData.skillsList[9] =  skill_Observation.GetBaseValue();
        _SaveData.skillsList[10] =  skill_Magic.GetBaseValue();
        _SaveData.skillsList[11] =  skill_Medicine.GetBaseValue();
        //Charisma
        _SaveData.skillsList[12] =  skill_Barter.GetBaseValue();
        _SaveData.skillsList[13] =  skill_Deception.GetBaseValue();
        _SaveData.skillsList[14] =  skill_Intimidation.GetBaseValue();
        _SaveData.skillsList[15] =  skill_Persuasion.GetBaseValue();

        return _SaveData;
    }
    public void RestoreState(object state)
    {
        gameManager.LoadingCharacter = true;
        var saveData = (SaveData)state;

        Vector3 newPosition = new Vector3(saveData.characterPosition[0], saveData.characterPosition[1], saveData.characterPosition[2]);
        Vector3 newRotation = new Vector3(saveData.characterRotation[0], saveData.characterRotation[1], saveData.characterRotation[2]);
        MoveCharacter(newPosition, newRotation);
        level = saveData.level;
        currentHealth = saveData.currentHealth;
        currentMagic = saveData.currentMagic;
        currentStamina = saveData.currentStamina;
        maxHealth.SetBaseValue(saveData.statsList[0]); maxMagic.SetBaseValue(saveData.statsList[1]); maxStamina.SetBaseValue(saveData.statsList[2]);
        damageResistance.SetBaseValue(saveData.statsList[3]); carryCapacity.SetBaseValue(saveData.statsList[4]);
        strength.SetBaseValue(saveData.statsList[5]); intelligence.SetBaseValue(saveData.statsList[6]); dexterity.SetBaseValue(saveData.statsList[7]);
        charisma.SetBaseValue(saveData.statsList[8]); constitution.SetBaseValue(saveData.statsList[9]);
    }
    [System.Serializable]
    private struct SaveData
    {

        public SaveData(float[] _characterPosition, float[] _characterRotation, int[] _statsList, int[] _skillsList)
        {
            characterPosition = _characterPosition;
            characterRotation = _characterRotation;

            level = 0;
            currentHealth = 0;
            currentMagic = 0;
            currentStamina = 0;

            statsList = _statsList;
            skillsList = _skillsList;
        }

        public float[] characterPosition;

        public float[] characterRotation;

        public int level;

        public int[] statsList; //= new int[10];

        public int[] skillsList; // = new int[16];
 
        public float currentHealth;
        public float currentMagic;
        public float currentStamina;

    }

    //Skill Checks
    public bool CheckSkill(Reply.SkillCheck skillCheck, int checkDC)
    {
        SetSkillsList();
        if (skillsList[(int)skillCheck] - 1 >= checkDC) return true;
        return false;
    }

    //Overrides
    public virtual void MoveCharacter(Vector3 position, Vector3 rotation){
        //for overriding, it won't work in the class by itself
    }
    public virtual void Die()
    {
        //Death effects occur
        //This method is just here to override in the individual Stats scripts
        Debug.Log(transform.name + " died");
    }
    public virtual void RefreshStats()
    {
        //resfresh stats and stuff. this is for overriding
    }
}
