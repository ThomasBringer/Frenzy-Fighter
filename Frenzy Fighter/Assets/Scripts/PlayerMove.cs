using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Joystick stick;

    Animator anim;

    [SerializeField] float moveSensitivity = 10;

    void Awake()
    {
        stick = FindObjectOfType<Joystick>();
        anim = GetComponentInChildren<Animator>();
    }

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
        if (velocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);
        anim.SetBool("running", velocity != Vector3.zero);
    }
}
