using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

// Player scores points when enemies are killed.
public class Score : MonoBehaviour
{
    TextMeshProUGUI text;

    int score = 0;
    [SerializeField] int bonusPerKill = 100;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        UpdateText();
    }

    void OnEnable()
    {
        Health.onDieAny.AddListener(OnEnemyKilled);
    }

    void OnDisable()
    {
        Health.onDieAny.RemoveListener(OnEnemyKilled);
    }

    void OnEnemyKilled(float damage, Health health)
    {
        score += bonusPerKill;
        UpdateText();
    }

    void UpdateText()
    {
        text.text = score + "";
    }
}
