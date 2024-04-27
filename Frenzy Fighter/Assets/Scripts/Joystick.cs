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

    Vector2 TouchPosition
    {
        get
        {
#if UNITY_EDITOR || UNITY_WEBGL
            return Input.mousePosition;
#else
            return Input.GetTouch(0).position;
#endif
        }
    }

    bool StartTouching
    {
        get
        {
#if UNITY_EDITOR || UNITY_WEBGL
            return Input.GetMouseButtonDown(0);
#else
            if (Input.touchCount == 0)
                return false;
            return Input.GetTouch(0).phase == TouchPhase.Began;
#endif
        }
    }

    bool EndTouching
    {
        get
        {
#if UNITY_EDITOR || UNITY_WEBGL
            return Input.GetMouseButtonUp(0);
#else
            if (Input.touchCount == 0)
                return false;
            return Input.GetTouch(0).phase == TouchPhase.Ended;
#endif
        }
    }

    void Update()
    {
        if (StartTouching)
        {
            StartTouch();
        }
        else if (EndTouching)
        {
            EndTouch();
        }
        else if (touching)
        {
            TouchingUpdate();
        }
    }

    void StartTouch()
    {
        screenStartTouch = TouchPosition;
        transform.position = screenStartTouch;
        touching = true;
    }

    void EndTouch()
    {
        innerStick.localPosition = Vector2.zero;
        touching = false;
    }

    void TouchingUpdate()
    {
        Vector2 target = TouchPosition;
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