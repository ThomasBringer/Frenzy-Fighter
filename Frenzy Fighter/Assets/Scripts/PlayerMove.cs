using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for the ability of the player to move. Reading input from a on-screen joystick.
public class PlayerMove : MonoBehaviour
{
    Joystick stick;

    Animator anim;
    PlayerAttack playerAttack;

    bool isRunning = false;

    [Tooltip("Max speed of the player, in world units per second.")]
    public float speed = 10;

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
            Vector2 velocity2D = -speed * stick.velocityPercent;
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

    // Triggered when the player starts or stops running.
    void IsRunningChange(bool isRunningNew)
    {
        anim.SetBool("running", isRunningNew);
        isRunning = isRunningNew;
        playerAttack.isRunning = isRunning;
        if (!isRunningNew) playerAttack.SelectTarget();
    }
}
