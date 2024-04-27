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

    // Damage the current character. Returns true if the character is killed.
    public bool Damage(float damage)
    {
        if (dead) return true;
        health -= damage;

        bool fatal = health <= 0;
        if (fatal)
            Die();
        else
            onDamage.Invoke();

        return fatal;
    }

    void Die()
    {
        dead = true;
        onDie.Invoke();
    }
}
