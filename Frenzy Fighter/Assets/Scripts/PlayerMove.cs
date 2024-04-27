using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Joystick stick;

    [SerializeField] float moveSensitivity = 10;

    void Awake()
    {
        stick = FindObjectOfType<Joystick>();
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
        transform.Translate(VelocityWorld * Time.deltaTime);
    }
}
