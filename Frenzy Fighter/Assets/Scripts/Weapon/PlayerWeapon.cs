using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for the ability of the player to equip weapons. Changing the stats of PlayerAttack and PlayerMove accordingly.
public class PlayerWeapon : MonoBehaviour
{
    PlayerAttack playerAttack;
    PlayerMove playerMove;
    Animator anim;

    [SerializeField] Transform hand;
    [Tooltip("Max distance for the player to grab a dropped weapon.")]
    [SerializeField] float weaponGrabRange = 2;

    GameObject currentWeapon;

    [SerializeField] Transform uiWeaponSlot;
    int uiLayer;

    GameObject currentWeaponUI;

    void Awake()
    {
        playerAttack = GetComponentInChildren<PlayerAttack>();
        playerMove = GetComponentInChildren<PlayerMove>();
        anim = GetComponentInChildren<Animator>();
        uiLayer = LayerMask.NameToLayer("UI");
    }

    public void Equip(Weapon weapon)
    {
        playerAttack.attackRange = weapon.attackRange;
        playerMove.speed = weapon.runSpeed;
        anim.SetFloat("fightSpeed", weapon.animationSpeed);
        currentWeapon = Instantiate(weapon.prefab, hand);
        EquipUI(weapon);
    }

    void Unequip()
    {
        ParticleSystem[] particles = currentWeapon.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particles)
        {
            particle.transform.SetParent(hand);
            particle.Stop();
            Destroy(particle, 10);
        }
        Destroy(currentWeapon);
        Destroy(currentWeaponUI);
    }

    void Reequip(Weapon weapon)
    {
        Unequip();
        Equip(weapon);
    }

    void LateUpdate()
    {
        bool found = WeaponsTracker.IsWeaponInRange(transform.position, weaponGrabRange);
        if (found)
        {
            WeaponItem weapon = WeaponsTracker.lastClosestWeapon;
            Destroy(weapon.gameObject);
            Reequip(weapon.weaponStats);
        }
    }

    void EquipUI(Weapon weapon)
    {
        currentWeaponUI = Instantiate(weapon.prefab, uiWeaponSlot);
        currentWeaponUI.layer = uiLayer;
    }
}