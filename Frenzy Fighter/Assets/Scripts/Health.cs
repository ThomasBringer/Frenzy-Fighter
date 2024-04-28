using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Class for all characters that have health. Such characters can be damaged and killed.
public class Health : MonoBehaviour
{
    [Tooltip("Amount of health of the character.")]
    [SerializeField] float health;

    [HideInInspector] public UnityEvent<float> onDamage = new UnityEvent<float>();
    [HideInInspector] public UnityEvent<float> onDie = new UnityEvent<float>();
    [HideInInspector] public bool dead = false;

    HealthBar healthBar;
    bool hasHealthBar;

    void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        hasHealthBar = healthBar != null;
        if (hasHealthBar)
            healthBar.InitHealth(health);
    }

    // Damage the current character. Returns true if the character is killed.
    public bool Damage(float damage)
    {
        if (dead) return true;
        health -= damage;

        TryUpdateHealthBar();

        bool fatal = health <= 0;
        if (fatal)
            Die(damage);
        else
            onDamage.Invoke(damage);

        return fatal;
    }

    void Die(float damage)
    {
        dead = true;
        onDie.Invoke(damage);
    }

    void TryUpdateHealthBar()
    {
        if (hasHealthBar)
            healthBar.SetHealth(health);
    }
}
