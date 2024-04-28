using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] Weapon[] weapons;
    Weapon RandomWeapon => weapons.RandomItem();

    [SerializeField] WeaponItem weaponItemPrefab;

    void Start()
    {
        PlayerWeapon playerWeapon = FindObjectOfType<PlayerWeapon>();
        playerWeapon.Equip(RandomWeapon);
    }

    public void SpawnWeapon(Vector3 position)
    {
        WeaponItem weaponItem = Instantiate(weaponItemPrefab, position, Quaternion.identity, transform);
        Weapon weaponStats = RandomWeapon;
        weaponItem.Initialize(weaponStats);
    }
}
