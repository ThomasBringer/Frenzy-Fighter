using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float damage = 10;

    public void Attack()
    {
        Enemy enemy = EnemiesTracker.GetClosestEnemy(transform.position);
        enemy.TryDamage(damage);
    }
}