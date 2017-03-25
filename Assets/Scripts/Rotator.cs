using System;
using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour {
    public Quaternion LastTargetedRotation { get; private set; }

    private void Awake()
    {
        LastTargetedRotation = transform.rotation;
    }

    public void Rotate (Quaternion targetedRotation, float duration = 0f)
    {
        StartCoroutine(RotateCoroutine(targetedRotation, duration));
    }

    private IEnumerator RotateCoroutine(Quaternion targetedRotation, float duration)
    {
        LastTargetedRotation = targetedRotation;
        Quaternion initialRotation = transform.rotation;
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            transform.rotation = Quaternion.Slerp(initialRotation, targetedRotation, counter / duration);

            yield return null;
        }

        transform.rotation = targetedRotation;
    }
}
