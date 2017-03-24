using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour {

    [SerializeField] private List<Button> buttons = new List<Button>();

    private List<int> buttonSequence;
    private int sequenceLenght = 1;

    private List<int> GetNewSequence(int sequenceLength)
    {
        buttonSequence = new List<int>();

        for (int i = 0; i < sequenceLength; i++)
            buttonSequence.Add(UnityEngine.Random.Range(0, buttons.Count));

        return buttonSequence;
    }

    // Use this for initialization
    void Start () {
        for (int i = buttons.Count -1; i >= 0; i--)
        {
            AddButtonClickListener(buttons[i], i);
        }
        StartCoroutine(PlaySequence(GetNewSequence(sequenceLenght)));    
    }

    private void AddButtonClickListener(Button button, int index)
    {
        button.onClick.AddListener(()=>
        {
            OnButtonClicked(index);
        });
    }

    private void OnButtonClicked(int index)
    {
        if (buttonSequence[0] == index)
        {
            buttonSequence.RemoveAt(0);
            if (buttonSequence.Count == 0)
            {
                Debug.ClearDeveloperConsole();
                Debug.Log("You WIN");
                
                StartCoroutine(PlaySequence(GetNewSequence(++sequenceLenght)));
            } 
        }
        else
        {
            Debug.Log("YOU LOSE !!!");
        }
    }

    private IEnumerator PlaySequence(List<int> sequence)
    {
        foreach (int index in sequence)
        {
            yield return BlinkButton(buttons[index]);
        }
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
