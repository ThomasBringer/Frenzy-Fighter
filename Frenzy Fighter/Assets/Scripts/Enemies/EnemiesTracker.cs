using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This class keeps track of which enemies are alive for the player to hit.
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

    // After using IsEnemyInRange(), if there was an enemy within the range, this was the closest enemy.
    public static Enemy lastClosestEnemy;

    // Is there an enemy within a certain range from a centre?
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
                // Finding an enemy
                lastClosestEnemy = enemy;
                minDistanceSquared = distanceSquared;
                found = true;
            }
        }
        return found;
    }
}
