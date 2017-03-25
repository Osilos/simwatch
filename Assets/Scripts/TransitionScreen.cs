using System;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : CanvasScreen
{
    [SerializeField] private Text sequenceTxt;

    public void SetSequenceID(int id)
    {
        sequenceTxt.text = id.ToString();
    }

    public void OpenShowSequenceAndClose(int sequenceId, Action actionOnEnd)
    {
        SetSequenceID(sequenceId);

        OpenTransitFromRight(() =>
        {
            CloseTransitToLeft(actionOnEnd);
        });
    }
}
