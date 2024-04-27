using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnEnable()
    {
        EnemiesTracker.Add(this);
    }
}
