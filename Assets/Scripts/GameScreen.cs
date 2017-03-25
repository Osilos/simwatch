using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void GameSceenColorClickEventHandler(GameScreen sender, int colorIdClicked);

public class GameScreen : CanvasScreen
{
    [SerializeField] private List<Button> buttons = new List<Button>();

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
