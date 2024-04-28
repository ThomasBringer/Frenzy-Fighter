using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    Animator anim;
    Health health;

    PlayerMove playerMove;
    PlayerAttack playerAttack;
    PlayerWeapon playerWeapon;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        health = GetComponentInChildren<Health>();

        playerMove = GetComponentInChildren<PlayerMove>();
        playerAttack = GetComponentInChildren<PlayerAttack>();
        playerWeapon = GetComponentInChildren<PlayerWeapon>();
    }

    void OnEnable()
    {
        health.onDie.AddListener(Die);
    }

    void OnDisable()
    {
        health.onDie.RemoveListener(Die);
    }

    void Die(float damage)
    {
        anim.SetTrigger("die");
        playerMove.enabled = false;
        playerAttack.enabled = false;
        playerWeapon.enabled = false;
    }
}
