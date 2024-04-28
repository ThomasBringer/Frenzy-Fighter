using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Class for all characters that have health. Such characters can be damaged and killed.
public class Health : MonoBehaviour
{
    [Tooltip("Amount of health of the character.")]
    public float health;

    [HideInInspector] public UnityEvent<float> onDamage = new UnityEvent<float>();
    [HideInInspector] public UnityEvent<float> onDie = new UnityEvent<float>();
    [HideInInspector] public UnityEvent<float> onHeal = new UnityEvent<float>();
    [HideInInspector] public static UnityEvent<float> onDamageAny = new UnityEvent<float>();
    [HideInInspector] public static UnityEvent<float, Health> onDieAny = new UnityEvent<float, Health>();
    [HideInInspector] public bool dead = false;

    // Damage the current character. Returns true if the character is killed.
    public bool Damage(float damage)
    {
        if (dead) return true;
        health -= damage;
        health = Mathf.Max(health, 0);

        bool fatal = health <= 0;
        if (fatal)
            Die(damage);
        else
        {
            onDamage.Invoke(damage);
            onDamageAny.Invoke(damage);
        }

        return fatal;
    }

    void Die(float damage)
    {
        dead = true;
        onDie.Invoke(damage);
        onDieAny.Invoke(damage, this);
    }

    public void Heal(float heal)
    {
        if (dead) return;
        health += heal;
        onHeal.Invoke(heal);
    }
}