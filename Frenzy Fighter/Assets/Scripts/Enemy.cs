using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    Health health;
    Animator anim;

    [Tooltip("After the enemy is killed, it will get destroyed after this delay.")]
    [SerializeField] float dieDestroyDelay = 5;

    void Awake()
    {
        health = GetComponent<Health>();
        anim = GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        EnemiesTracker.Add(this);
        health.onDie.AddListener(Die);
        health.onDamage.AddListener(OnDamage);
    }

    void OnDisable()
    {
        health.onDie.RemoveListener(Die);
        health.onDamage.RemoveListener(OnDamage);
    }

    public bool TryDamage(float damage)
    {
        return health.Damage(damage);
    }

    public void OnDamage()
    {
        anim.SetTrigger("damage");
    }

    void Die()
    {
        anim.SetTrigger("die");
        EnemiesTracker.Remove(this);
        Destroy(gameObject, dieDestroyDelay);
    }
}
