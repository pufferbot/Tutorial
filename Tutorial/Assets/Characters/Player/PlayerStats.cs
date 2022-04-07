using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField] PlayerUI playerUI;
    public ItemInstance testItem;

    void Start()
    {
        maxHealth.AttributeBonus(constitution.GetValue(), basicStatMultiplier, basicStatAdditioner); //50-100 base hp
        maxMagic.AttributeBonus(intelligence.GetValue(), basicStatMultiplier, basicStatAdditioner); //50-100 base magicyness
        maxStamina.AttributeBonus(strength.GetValue(), basicStatMultiplier, basicStatAdditioner); //50-100 base stamina

        SetStats();
        SetInitialSkills();
    }

    void LateUpdate()
    {
        characterPosition = transform.position;
        characterRotation = transform.eulerAngles;
        if(gameManager.GetGameState() != GameManager.GameState.Running)
            return;
        if(gameManager.LoadingCharacter == true)
            gameManager.LoadingCharacter = false;
        RefreshStats();
        StartCoroutine(RegenStats());

    }

    public override void RefreshStats()
    {
        if(carryCapacity.GetValue() < inventory.TotalWeight())
        {
            overEncumbered = true;
        }
        playerUI.SetHealth(((float)maxHealth.GetValue()) / ((float) currentHealth));
        playerUI.SetMagic(((float)maxMagic.GetValue()) / ((float) currentMagic));
        playerUI.SetStamina(((float)maxStamina.GetValue()) / ((float)currentStamina));
    }

    public override void MoveCharacter(Vector3 position, Vector3 rotation){
        playerUI.TogglePauseMenu();
        gameManager.SetCharacterPosition(gameObject, position, rotation);
    }

    public override void Die()
    {
        
    }
}
