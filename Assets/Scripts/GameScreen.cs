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

    public void PlaySequence(List<int> sequence)
    {
        StartCoroutine(PlaySequenceCoroutine(sequence));
    }

    private IEnumerator PlaySequenceCoroutine(List<int> sequence)
    {
        foreach (int index in sequence)
            yield return BlinkButton(buttons[index]);
    }

    private IEnumerator BlinkButton (Button button)
    {
        yield return new WaitForSeconds(0.2f);
        Color currentColor = button.image.color;
        button.image.color = Color.white;
        yield return new WaitForSeconds(0.3f);
        button.image.color = currentColor;
    }
}
