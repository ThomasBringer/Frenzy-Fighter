using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    Vector2 screenStartTouch = Vector2.zero;
    bool touching = false;

    Transform innerStick;

    [SerializeField] float maxStickDistance = 37.5f;
    float maxStickDistanceSquared;

    void Awake()
    {
        innerStick = transform.GetChild(0);
        maxStickDistanceSquared = maxStickDistance * maxStickDistance;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartTouching();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndTouching();
        }
        else if (touching)
        {
            TouchingUpdate();
        }
    }

    void StartTouching()
    {
        screenStartTouch = Input.mousePosition;
        transform.position = screenStartTouch;
        touching = true;
    }

    void EndTouching()
    {
        innerStick.localPosition = Vector2.zero;
        touching = false;
    }

    void TouchingUpdate()
    {
        Vector2 target = Input.mousePosition;
        Vector2 dir = target - screenStartTouch;
        float distanceSquared = Vector2.SqrMagnitude(dir);
        if (distanceSquared > maxStickDistanceSquared)
        {
            target = screenStartTouch + maxStickDistance * dir.normalized;
        }
        innerStick.position = target;
    }
}