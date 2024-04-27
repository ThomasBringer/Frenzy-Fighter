using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    Health health;
    Animator anim;

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
        Debug.Log("damage");
        anim.SetTrigger("damage");
    }

    void Die()
    {
        Debug.Log("die");
        anim.SetTrigger("die");
        EnemiesTracker.Remove(this);
    }
}
