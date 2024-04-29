using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChromaticAberrationTween : MonoBehaviour
{
    VolumeProfile volumeProfile;
    ChromaticAberration chromaticAberration;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float duration;
    bool elementFound;
    [SerializeField] float amount = .5f;

    void Awake()
    {
        volumeProfile = FindObjectOfType<Volume>().profile;
        elementFound = volumeProfile.TryGet(out chromaticAberration);
    }

    public void PlayEffect()
    {
        if (elementFound)
            StartCoroutine(ChangeChromaticAberration());
    }

    IEnumerator ChangeChromaticAberration()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float newValue = amount * curve.Evaluate(t);
            chromaticAberration.intensity.value = newValue;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        chromaticAberration.intensity.value = 0;
    }
}
