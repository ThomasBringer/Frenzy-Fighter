using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    Renderer rendrr;
    [SerializeField] Color color;
    [SerializeField] float duration = .2f;

    void Awake()
    {
        rendrr = GetComponentInChildren<Renderer>();
        rendrr.material.EnableKeyword("_EMISSION");
    }

    public void FlashIn()
    {
        rendrr.material.SetColor("_EmissionColor", color);
        Invoke(nameof(FlashOut), duration);
    }

    void FlashOut()
    {
        rendrr.material.SetColor("_EmissionColor", Color.black);
    }
}
