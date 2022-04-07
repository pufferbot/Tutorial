using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacterStats : CharacterStats
{

    [SerializeField] NPCHealthBar nPCHealthBar;

    void Start()
    {
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
        nPCHealthBar.SetHealth(((float)maxHealth.GetValue()) / ((float) currentHealth));
    }

    public override void MoveCharacter(Vector3 position, Vector3 rotation){
        gameManager.SetCharacterPosition(gameObject, position, rotation);
    }

    public override void Die()
    {
        
    }
}
