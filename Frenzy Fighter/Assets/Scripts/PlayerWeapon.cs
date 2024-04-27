using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Weapon[] weapons;

    PlayerAttack playerAttack;
    PlayerMove playerMove;
    Animator anim;

    void Awake()
    {
        playerAttack = GetComponentInChildren<PlayerAttack>();
        playerMove = GetComponentInChildren<PlayerMove>();
        anim = GetComponentInChildren<Animator>();
        Equip(weapons.RandomItem());
    }

    void Equip(Weapon weapon)
    {
        playerAttack.attackRange = weapon.attackRange;
        playerMove.speed = weapon.runSpeed;
        anim.SetFloat("fightSpeed", weapon.animationSpeed);
    }
}

public static class Extensions
{
    public static T RandomItem<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}