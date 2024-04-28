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
        // Listen to enemies being added.
        EnemiesTracker.onEnemyAdded.AddListener(OnEnemyAdded);
    }

    void OnDisable()
    {
        EnemiesTracker.onEnemyAdded.RemoveListener(OnEnemyAdded);
    }

    void OnEnemyAdded(Enemy enemy)
    {
        // When enemy is added, listen when he is killed.
        enemy.health.onDie.AddListener(OnEnemyKilled);
    }

    void OnEnemyKilled(float damage, Health health)
    {
        score += bonusPerKill;
        UpdateText();
        // Enemy is dead, we can stop listening to him being killed.
        health.onDie.RemoveListener(OnEnemyKilled);
    }

    void UpdateText()
    {
        text.text = score + "";
    }
}
