using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for all enemies.
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    Health health;
    Animator anim;

    [Tooltip("After the enemy is killed, it will get destroyed after this delay.")]
    [SerializeField] float dieDestroyDelay = 5;

    PlayerMove player;

    [SerializeField] Transform graphics;

    void Awake()
    {
        health = GetComponent<Health>();
        anim = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerMove>();
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

    public void OnDamage(float damage)
    {
        anim.SetTrigger("damage");
    }

    void Die(float damage)
    {
        anim.SetTrigger("die");
        EnemiesTracker.Remove(this);
        Destroy(gameObject, dieDestroyDelay);
    }

    void Update()
    {
        if (!health.dead)
            graphics.LookAt(player.transform);
    }
}
