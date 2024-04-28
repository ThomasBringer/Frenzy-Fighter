using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for spawning enemies. Enemy will get spawned within the bounds every random delay.
public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Min time between 2 enemy spawns.")]
    [SerializeField] float minTimeBetweenSpawns = .5f;
    [Tooltip("Max time between 2 enemy spawns.")]
    [SerializeField] float maxTimeBetweenSpawns = 5;

    float TimeBetweenSpawns => Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);

    [Tooltip("Bounds of the map to spawn enemies.")]
    [SerializeField] Vector2 spawnBounds;

    Vector3 SpawnPoint => .5f * new Vector3(Random.Range(-spawnBounds.x, spawnBounds.x), 0, Random.Range(-spawnBounds.y, spawnBounds.y));

    [SerializeField] Enemy enemyPrefab;

    [Tooltip("Max number of enemies on the map at the same time.")]
    [SerializeField] int maxEnemyCount = 12;

    void Awake()
    {
        SpawnRepeating();
    }

    void SpawnRepeating()
    {
        TrySpawn();
        Invoke(nameof(SpawnRepeating), TimeBetweenSpawns);
    }

    void TrySpawn()
    {
        if (EnemiesTracker.EnemyCount < maxEnemyCount)
            Spawn();
    }

    void Spawn()
    {
        Instantiate(enemyPrefab, SpawnPoint, Quaternion.identity, transform);
    }
}