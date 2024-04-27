using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemiesTracker
{
    static List<Enemy> enemies;

    public static void Add(Enemy enemy)
    {
        if (enemies == null)
        {
            enemies = new List<Enemy>();
        }
        enemies.Add(enemy);
    }

    public static void Remove(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    public static Enemy lastClosestEnemy;

    public static bool IsEnemyInRange(Vector3 centre, float range)
    {
        if (enemies == null) return false;

        float minDistanceSquared = Mathf.Infinity;
        float rangeSquared = range * range;

        bool found = false;
        foreach (Enemy enemy in enemies)
        {
            float distanceSquared = Vector3.SqrMagnitude(enemy.transform.position - centre);

            // If enemy is in range AND closer than other enemies so far
            if (distanceSquared < Mathf.Min(rangeSquared, minDistanceSquared))
            {
                lastClosestEnemy = enemy;
                minDistanceSquared = distanceSquared;
                found = true;
            }
        }
        return found;
    }
}
