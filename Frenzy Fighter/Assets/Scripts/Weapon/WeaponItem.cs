using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] Transform weaponSlot;

    [HideInInspector] public Weapon weaponStats;

    public void Initialize(Weapon weaponStats)
    {
        this.weaponStats = weaponStats;
        Instantiate(weaponStats.prefab, weaponSlot);
    }

    void OnEnable()
    {
        WeaponsTracker.Add(this);
    }

    void OnDisable()
    {
        WeaponsTracker.Remove(this);
    }
}
