using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] Transform weaponSlot;

    public void Initialize(Weapon weaponStats)
    {
        Instantiate(weaponStats.prefab, weaponSlot);
    }
}
