using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] float health;

    public UnityEvent onDamage = new UnityEvent();
    public UnityEvent onDie = new UnityEvent();
    bool dead = false;

    public void Damage(float damage)
    {
        if (dead) return;
        health -= damage;
        if (health <= 0)
            Die();
        else
            onDamage.Invoke();
    }

    void Die()
    {
        dead = true;
        onDie.Invoke();
    }
}
