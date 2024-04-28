using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Damage shown when enemy is hit
public class DamageText : MonoBehaviour
{
    Animator anim;
    [SerializeField] Health health;
    TextMeshProUGUI text;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        health.onDie.AddListener(OnDieOrDamage);
        health.onDamage.AddListener(OnDieOrDamage);
    }

    void OnDisable()
    {
        health.onDie.RemoveListener(OnDieOrDamage);
        health.onDamage.RemoveListener(OnDieOrDamage);
    }

    void OnDieOrDamage(float damage, Health health)
    {
        text.text = damage + "";
        Show();
    }

    void Show()
    {
        anim.SetTrigger("show");
    }
}
