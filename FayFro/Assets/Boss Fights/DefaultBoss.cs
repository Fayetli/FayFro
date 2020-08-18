using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class DefaultBoss : ActivateObject
{
    [SerializeField] protected int _hp;

    public override void Activate()
    {
        StartAttack();
    }

    public abstract void StartAttack();

    public abstract void FinishAttack();

    public abstract void TakeDamage();

    public abstract int GetHP();

}
