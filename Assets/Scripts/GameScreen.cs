using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public delegate void GameSceenColorClickEventHandler(GameScreen sender, int colorIdClicked);

public class GameScreen : CanvasScreen
{
    [SerializeField] private List<ColorButton> buttons = new List<ColorButton>();
    [SerializeField] private ColorButton rotationButton;
    [SerializeField] private Rotator rotator;
    [SerializeField] private float rotationSecondsDuration;
    [SerializeField] private float blinkSecondsDuration = 0.3f;
    [SerializeField] private GameObject error;
    [SerializeField] private float secondsDelayBetweenBlink = 0.2f;
    [SerializeField] private float secondsDelayBeforeFirstSequence = 0.4f;

    private bool _enableClick = true;
    public bool EnableClick
    {
        get
        {
            return _enableClick;
        }
        set
        {
            _enableClick = value;
            
            for (int i = buttons.Count-1; i > -1; i--)
                buttons[i].interactable = value;
        }
    }

    public int ColorCount {
        get
        {
            return buttons.Count;
        }
    }

    public event GameSceenColorClickEventHandler OnColorClicked;

    private void Start () {
        for (int i = buttons.Count -1; i >= 0; i--)
            AddButtonClickListener(buttons[i], i);

        rotationButton.onClick.AddListener(RotationButton_OnClicked);
    }

    public void SetErrorVisible(bool isVisible)
    {
        error.SetActive(isVisible);
    }

    private void RotationButton_OnClicked()
    {
        rotator.Rotate(Quaternion.AngleAxis(90 * Random.Range(1, 4), Vector3.forward) * rotator.LastTargetedRotation, rotationSecondsDuration);
    }

    private void AddButtonClickListener(Button button, int index)
    {
        button.onClick.AddListener(() => {
            if (OnColorClicked != null)
                OnColorClicked(this, index);
        });
    }

    public void PlaySequence(List<int> sequence, Action actionOnEnd = null, bool isFirstSequence = false)
    {
        StartCoroutine(PlaySequenceCoroutine(sequence, actionOnEnd, isFirstSequence));
    }

    private IEnumerator PlaySequenceCoroutine(List<int> sequence, Action actionOnEnd = null, bool isFirstSequence = false)
    {
        if (isFirstSequence)
            yield return new WaitForSeconds(secondsDelayBeforeFirstSequence);

        for (int i = 0; i < sequence.Count; i++)
        {
            yield return new WaitForSeconds(secondsDelayBetweenBlink);
            yield return buttons[sequence[i]].Blink(i + 1 , blinkSecondsDuration);
        }

        if (actionOnEnd != null)
            actionOnEnd();
    }
}
