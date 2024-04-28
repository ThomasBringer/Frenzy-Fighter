using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class handling player death
public class PlayerDie : LoadScene
{
    Animator anim;
    Health health;

    PlayerMove playerMove;
    PlayerAttack playerAttack;
    PlayerWeapon playerWeapon;

    [Tooltip("Once player is dead, the game over scene will load after this delay, in seconds.")]
    [SerializeField] float delayLoadGameOverScene = 4;

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

        EnemiesTracker.Clear();
        WeaponsTracker.Clear();

        Invoke(nameof(LoadGameOver), delayLoadGameOverScene);
    }

    void LoadGameOver() => Load("Game Over Menu");
}
