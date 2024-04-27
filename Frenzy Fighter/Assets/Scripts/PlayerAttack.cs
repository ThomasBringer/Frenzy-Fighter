using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float damage = 10;
    Enemy target;

    public void Attack()
    {
        bool killed = target.TryDamage(damage);
        if (killed) // if the target died, select a new target
            SelectTarget();
    }

    public void SelectTarget()
    {
        target = EnemiesTracker.GetClosestEnemy(transform.position);
    }
}