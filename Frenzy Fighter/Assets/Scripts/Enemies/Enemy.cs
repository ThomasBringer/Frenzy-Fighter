using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Class for all enemies.
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    Health health;
    Animator anim;

    [Tooltip("After the enemy is killed, it will get destroyed after this delay.")]
    [SerializeField] float dieDestroyDelay = 5;

    PlayerMove player;

    [SerializeField] Transform graphics;

    NavMeshAgent agent;

    [SerializeField] float updatePlayerTargetDelay = .5f;

    void Awake()
    {
        health = GetComponent<Health>();
        anim = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerMove>();
        agent = GetComponentInChildren<NavMeshAgent>();
    }

    void Start()
    {
        InvokeRepeating(nameof(SetAgentDestination), 0, updatePlayerTargetDelay);
    }

    void OnEnable()
    {
        EnemiesTracker.Add(this);
        health.onDie.AddListener(Die);
        health.onDamage.AddListener(OnDamage);
    }

    void OnDisable()
    {
        health.onDie.RemoveListener(Die);
        health.onDamage.RemoveListener(OnDamage);
    }

    public bool TryDamage(float damage)
    {
        return health.Damage(damage);
    }

    public void OnDamage(float damage)
    {
        anim.SetTrigger("damage");
    }

    void Die(float damage)
    {
        anim.SetTrigger("die");
        EnemiesTracker.Remove(this);
        Destroy(gameObject, dieDestroyDelay);
        StopAgent();
    }

    void Update()
    {
        if (!health.dead)
            AliveUpdate();
    }

    void AliveUpdate()
    {
        // Look towards player (but ignore Y-axis)
        graphics.LookAt(transform.position + Vector3.Scale(player.transform.position - transform.position, new Vector3(1, 0, 1)));
    }

    void SetAgentDestination()
    {
        agent.SetDestination(player.transform.position);
    }

    void StopAgent()
    {
        agent.isStopped = true;
        CancelInvoke(nameof(SetAgentDestination));
    }
}
