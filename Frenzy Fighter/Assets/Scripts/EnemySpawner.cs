using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        SpawnRepeating();
    }

    void SpawnRepeating()
    {
        Spawn();
        Invoke(nameof(SpawnRepeating), TimeBetweenSpawns);
    }

    void Spawn()
    {
        Instantiate(enemyPrefab, SpawnPoint, Quaternion.identity, transform);
    }

}