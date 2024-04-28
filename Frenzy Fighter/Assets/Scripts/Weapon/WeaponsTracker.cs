using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class keeps track of which weapons have been dropped
public static class WeaponsTracker
{
    static List<WeaponItem> weapons;

    public static void Add(WeaponItem weapon)
    {
        if (weapons == null)
        {
            weapons = new List<WeaponItem>();
        }
        weapons.Add(weapon);
    }

    public static void Remove(WeaponItem weapon)
    {
        weapons.Remove(weapon);
    }

    // After using IsWeaponInRange(), if there was a weapon within the range, this was the closest weapon.
    public static WeaponItem lastClosestWeapon;

    // Is there a weapon within a certain range from a centre?
    public static bool IsWeaponInRange(Vector3 centre, float range)
    {
        if (weapons == null) return false;

        float minDistanceSquared = Mathf.Infinity;
        float rangeSquared = range * range;

        bool found = false;
        foreach (WeaponItem weapon in weapons)
        {
            float distanceSquared = Vector3.SqrMagnitude(weapon.transform.position - centre);

            // If weapon is in range AND closer than other weapons so far
            if (distanceSquared < Mathf.Min(rangeSquared, minDistanceSquared))
            {
                // Finding a weapon
                lastClosestWeapon = weapon;
                minDistanceSquared = distanceSquared;
                found = true;
            }
        }
        return found;
    }

    public static void Clear()
    {
        weapons.Clear();
    }
}