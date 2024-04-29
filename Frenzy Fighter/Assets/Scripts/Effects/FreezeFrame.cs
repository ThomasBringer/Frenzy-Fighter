using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeFrame : MonoBehaviour
{
    [SerializeField] float delay;

    public void Freeze()
    {
        Time.timeScale = .1f;
        StartCoroutine(WaitAndDefreeze());
    }

    IEnumerator WaitAndDefreeze()
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1;
    }
}
