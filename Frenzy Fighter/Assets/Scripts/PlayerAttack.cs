using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    void Awake()
    {
        Attack();
    }

    public void AttackChange(bool isAttacking)
    {

    }

    void Attack()
    {
        Enemy enemy = EnemiesTracker.GetClosestEnemy(transform.position);
    }
}