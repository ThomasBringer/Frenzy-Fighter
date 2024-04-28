using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class for health bars
public class HealthBar : MonoBehaviour
{
    Slider slider;

    public void InitHealth(float maxValue)
    {
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = maxValue;
        slider.minValue = 0;
        slider.value = maxValue;
    }

    public void SetHealth(float value)
    {
        slider.value = value;
    }
}