using System;
using System.Collections;
using UnityEngine;

public class CanvasScreen : MonoBehaviour
{
    [SerializeField] private float transitionSecondsDuration = 0.2f;

    private Vector3 openedPosition;

    private void Awake()
    {
        openedPosition = transform.position;
    }

    public void OpenTransitFromRight(Action actionOnEnd = null)
    {
        Open();
        transform.position = openedPosition + Vector3.right * Screen.width;
        StartCoroutine(GoToPositionAndPerform(openedPosition, transitionSecondsDuration, actionOnEnd));
    }

    private IEnumerator GoToPositionAndPerform(Vector3 targetedPosition, float transitionDuration, Action actionOnEnd = null)
    {
        float timer = 0;
        Vector3 initialPosition = transform.position;

        while (timer < transitionDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetedPosition, timer / transitionDuration);
            yield return null;
            timer += Time.deltaTime;
        }

        transform.position = targetedPosition;

        if (actionOnEnd != null)
            actionOnEnd();
    }

    public void CloseTransitToLeft(Action actionOnEnd = null)
    {
        StartCoroutine(GoToPositionAndPerform(openedPosition + Vector3.left * Screen.width, transitionSecondsDuration, ()=>
        {
            Close();

            if (actionOnEnd != null)
                actionOnEnd();
        }));
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
