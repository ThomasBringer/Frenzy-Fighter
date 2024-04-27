using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Joystick stick;

    Animator anim;
    PlayerAttack playerAttack;

    bool isRunning = false;

    [SerializeField] float moveSensitivity = 10;

    void Awake()
    {
        stick = FindObjectOfType<Joystick>();
        anim = GetComponentInChildren<Animator>();
        playerAttack = GetComponentInChildren<PlayerAttack>();
    }

    void Start() => IsRunningChange(false);

    Vector3 VelocityWorld
    {
        get
        {
            Vector2 velocity2D = -moveSensitivity * stick.velocityPercent;
            return new Vector3(velocity2D.x, 0, velocity2D.y);
        }
    }

    void LateUpdate()
    {
        Vector3 velocity = VelocityWorld;
        transform.Translate(velocity * Time.deltaTime, Space.World);

        bool isRunningNew = velocity != Vector3.zero;

        if (isRunningNew)
            transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);

        if (isRunningNew != isRunning)
            IsRunningChange(isRunningNew);
    }

    // Triggered when the playeer starts or stops running.
    void IsRunningChange(bool isRunningNew)
    {
        anim.SetBool("running", isRunningNew);
        isRunning = isRunningNew;
        if (!isRunningNew) // if the player stops running, select a new target
            playerAttack.SelectTarget();
    }
}
