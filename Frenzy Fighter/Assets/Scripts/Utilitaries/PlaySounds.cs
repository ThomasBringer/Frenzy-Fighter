using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    [SerializeField] AudioSource hitSource;
    [SerializeField] AudioSource swingSource;
    [SerializeField] AudioSource swordSource;
    [SerializeField] AudioSource dieSource;
    [SerializeField] AudioSource footSource;

    public void PlayHit() => hitSource.Play();
    public void PlaySwing() => swingSource.Play();
    public void PlaySword() => swordSource.Play();
    public void PlayDead() => dieSource.Play();
    public void PlayFoot() => footSource.Play();
}
