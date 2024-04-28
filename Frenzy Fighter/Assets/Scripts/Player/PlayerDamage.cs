using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    Animator anim;
    Health health;

    Renderer rendrr;
    [SerializeField] float flashWhiteDuration = .1f;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        health = GetComponentInChildren<Health>();

        rendrr = GetComponentInChildren<Renderer>();
        rendrr.material.EnableKeyword("_EMISSION");
    }

    void OnEnable()
    {
        health.onDamage.AddListener(Damage);
        health.onDamage.AddListener(FlashIn);
        health.onDie.AddListener(FlashIn);
    }

    void OnDisable()
    {
        health.onDamage.RemoveListener(Damage);
        health.onDamage.RemoveListener(FlashIn);
        health.onDie.RemoveListener(FlashIn);
    }

    void Damage(float damage)
    {
        anim.SetTrigger("damage");
    }

    void FlashIn(float damage)
    {
        rendrr.material.SetColor("_EmissionColor", Color.red);
        Invoke(nameof(FlashOut), flashWhiteDuration);
    }

    void FlashOut()
    {
        rendrr.material.SetColor("_EmissionColor", Color.black);
    }
}