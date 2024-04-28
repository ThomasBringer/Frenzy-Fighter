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

    void Awake()
    {
        playerAttack = GetComponentInChildren<PlayerAttack>();
        playerMove = GetComponentInChildren<PlayerMove>();
        anim = GetComponentInChildren<Animator>();
    }

    public void Equip(Weapon weapon)
    {
        playerAttack.attackRange = weapon.attackRange;
        playerMove.speed = weapon.runSpeed;
        anim.SetFloat("fightSpeed", weapon.animationSpeed);
        Instantiate(weapon.prefab, hand);
    }
}