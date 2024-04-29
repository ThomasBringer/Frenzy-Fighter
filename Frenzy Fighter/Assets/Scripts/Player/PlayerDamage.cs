using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    Animator anim;
    Health health;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        health = GetComponentInChildren<Health>();
    }

    void OnEnable()
    {
        health.onDamage.AddListener(Damage);
    }

    void OnDisable()
    {
        health.onDamage.RemoveListener(Damage);
    }

    void Damage(float damage)
    {
        anim.SetTrigger("damage");
    }
}