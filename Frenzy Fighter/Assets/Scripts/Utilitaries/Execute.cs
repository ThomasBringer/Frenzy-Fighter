using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Execute : MonoBehaviour
{
    public UnityEvent myEvent = new UnityEvent();

    public void Invoke() => myEvent.Invoke();
}
