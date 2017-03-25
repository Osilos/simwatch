using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public delegate void GameSceenColorClickEventHandler(GameScreen sender, int colorIdClicked);

public class GameScreen : CanvasScreen
{
    [SerializeField] private List<Button> buttons = new List<Button>();
    [SerializeField] private Button rotationButton;
    [SerializeField] private Rotator rotator;
    [SerializeField] private float rotationSecondsDuration;
    [SerializeField] private float blinkSecondsDuration = 0.3f;
    [SerializeField] private GameObject error;

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
        rotator.Rotate(Quaternion.AngleAxis(90 * (Random.value > 0.5 ? -1 : 1), Vector3.forward) * rotator.transform.rotation, rotationSecondsDuration);
    }

    private void AddButtonClickListener(Button button, int index)
    {
        button.onClick.AddListener(() => {
            if (OnColorClicked != null)
                OnColorClicked(this, index);
        });
    }

    public void PlaySequence(List<int> sequence, Action actionOnEnd = null)
    {
        StartCoroutine(PlaySequenceCoroutine(sequence, actionOnEnd));
    }

    private IEnumerator PlaySequenceCoroutine(List<int> sequence, Action actionOnEnd = null)
    {
        foreach (int index in sequence)
            yield return BlinkButton(buttons[index]);

        if (actionOnEnd != null)
            actionOnEnd();
    }

    private IEnumerator BlinkButton (Button button)
    {
        yield return new WaitForSeconds(0.2f);
        ColorBlock initialColorBlock = button.colors;
        ColorBlock newColorBlock     = button.colors;

        newColorBlock.disabledColor = newColorBlock.normalColor = button.colors.pressedColor;
        button.colors = newColorBlock;
        yield return new WaitForSeconds(blinkSecondsDuration);
        button.colors = initialColorBlock;

    }
}
