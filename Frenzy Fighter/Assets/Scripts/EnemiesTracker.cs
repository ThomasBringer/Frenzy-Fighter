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

    public static Enemy GetClosestEnemy(Vector3 centre)
    {
        float minDistanceSquared = Mathf.Infinity;
        Enemy closestEnemy = null;
        foreach (Enemy enemy in enemies)
        {
            float distanceSquared = Vector3.SqrMagnitude(enemy.transform.position - centre);
            if (distanceSquared < minDistanceSquared)
            {
                closestEnemy = enemy;
                minDistanceSquared = distanceSquared;
            }
        }
        return closestEnemy;
    }
}
