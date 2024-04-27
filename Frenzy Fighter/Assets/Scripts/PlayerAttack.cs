using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Tooltip("Damage of each player attack.")]
    [SerializeField] float damage = 10;

    [Tooltip("Max distance the player can hit an enemy, in world units.")]
    public float attackRange = 5;
    Enemy target = null;

    Animator anim;
    [HideInInspector] public bool isRunning;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Attack()
    {
        if (target == null)
            return;
        bool killed = target.TryDamage(damage);

        if (killed) // if the target died, select a new target
            SelectTarget();
    }

    void Update()
    {
        if (!isRunning)
            TrySelectTarget();
    }

    void TrySelectTarget()
    {
        if (target == null)
            SelectTarget();
    }

    public void SelectTarget()
    {
        bool found = EnemiesTracker.IsEnemyInRange(transform.position, attackRange); // search a target

        anim.SetBool("attacking", found);

        target = found ? EnemiesTracker.lastClosestEnemy : null;
    }
}