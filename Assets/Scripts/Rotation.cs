using System.Collections;
using UnityEngine;

public class Rotation : MonoBehaviour {

    public void Rotate (float duration = 0.5f)
    {
        StartCoroutine(RotateCoroutine(duration));
    }

    private IEnumerator RotateCoroutine(float duration)
    {
        Quaternion finalRotation = Quaternion.AngleAxis(90 * (Random.value > 0.5 ? -1 : 1), Vector3.forward) * transform.rotation;
        Quaternion initialRotation = transform.rotation;
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            transform.rotation = Quaternion.Slerp(initialRotation, finalRotation, counter / duration);

            yield return null;
        }

        transform.rotation = finalRotation;
    }
}
