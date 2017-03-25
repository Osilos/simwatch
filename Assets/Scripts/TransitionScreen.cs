using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour {
    [SerializeField] private Text sequenceTxt;

    public void SetSequenceID(int id)
    {
        sequenceTxt.text = id.ToString();
    }
}
