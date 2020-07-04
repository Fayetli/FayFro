using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class DefaultBoss : MonoBehaviour
{
    [SerializeField] protected int hp;


    public abstract void StartAttack();

    public abstract void FinishAttack();

}
