using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackTrigger : MonoBehaviour
{
    public void AttackTrigger()
    {
        GetComponentInParent<WeaponControls>().MeleeAttack();
    }

    public void FinishAttack()
    {
        GetComponentInParent<WeaponControls>().isAttacking = false;
    }
}
