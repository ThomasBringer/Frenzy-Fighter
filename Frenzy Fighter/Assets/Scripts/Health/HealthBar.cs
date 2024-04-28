using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class for health bars
public class HealthBar : MonoBehaviour
{
    Slider slider;

    [SerializeField] Health health;

    void Start()
    {
        InitHealth(health.health);
    }

    public void InitHealth(float maxValue)
    {
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = maxValue;
        slider.minValue = 0;
        slider.value = maxValue;
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

    void OnDieOrDamage(float damage, Health h)
    {
        SetHealth(health.health);
    }

    public void SetHealth(float value)
    {
        slider.value = value;
    }
}