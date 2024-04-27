using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    Vector2 screenStartTouch = Vector2.zero;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            screenStartTouch = Input.mousePosition;
            transform.position = screenStartTouch;
        }
    }
}