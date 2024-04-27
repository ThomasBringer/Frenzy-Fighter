using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    Vector2 screenStartTouch = Vector2.zero; // Position at which the user starts touching the screen.
    bool touching = false; // Is the user currently touching the screen?

    Transform innerStick;

    [Tooltip("Max distance the inner stick can move from the origin, in screen pixels.")]
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

        // If the touch position is too far away from the origin of the joystick:
        // (Distance check is done using squared values for optimisation)
        if (distanceSquared > maxStickDistanceSquared)
        {
            target = screenStartTouch + maxStickDistance * dir.normalized;
        }
        innerStick.position = target;
    }
}