using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [Tooltip("Graphics of the weapon.")]
    public GameObject prefab;

    [Tooltip("Speed of a strike animation when the weapon is equipped.")]
    public float animationSpeed;

    [Tooltip("Max player run speed when the weapon is equipped.")]
    public float runSpeed;

    [Tooltip("Max distance at which player can hit enemies when the weapon is equipped.")]
    public float attackRange;
}