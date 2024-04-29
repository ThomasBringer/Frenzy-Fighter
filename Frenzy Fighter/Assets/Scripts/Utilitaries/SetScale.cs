using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScale : MonoBehaviour
{
    [SerializeField] Vector3 scale = new Vector3(1, 1, 1);

    void Awake()
    {
        Transform parent = transform.parent;
        transform.SetParent(null);
        transform.localScale = scale;
        transform.SetParent(parent);
    }
}
