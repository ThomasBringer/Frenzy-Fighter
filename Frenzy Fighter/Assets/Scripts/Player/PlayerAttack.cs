using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for the ability of the player to attack. Can select enemy targets within a range and and attack them.
public class PlayerAttack : MonoBehaviour
{
    [Tooltip("Damage of each player attack.")]
    [SerializeField] float damage = 10;

    [Tooltip("Max distance the player can hit an enemy, in world units.")]
    public float attackRange = 5;
    Enemy target = null;

    Animator anim;
    [HideInInspector] public bool isRunning;

    [SerializeField] Transform graphics;

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
        if (isRunning) return;

        TrySelectTarget();
        TryLookAtTarget();
    }

    void TrySelectTarget()
    {
        if (target == null)
            SelectTarget();
    }

    void TryLookAtTarget()
    {
        if (target != null)
        {
            // Look towards target (but ignore Y-axis)
            graphics.LookAt(transform.position + Vector3.Scale(target.transform.position - transform.position, new Vector3(1, 0, 1)));
        }
    }

    public void SelectTarget()
    {
        bool found = EnemiesTracker.IsEnemyInRange(transform.position, attackRange); // search a target

        anim.SetBool("attacking", found);

        target = found ? EnemiesTracker.lastClosestEnemy : null;
    }
}