using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public GameObject prefab;
    public float animationSpeed;
    public float runSpeed;
    public float attackRange;
}