using UnityEngine;

public class ColorScaler : MonoBehaviour {
	private void Start () {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float size                  = Mathf.Sqrt(Mathf.Pow(Mathf.Max(Screen.width, Screen.height), 2) * 2)/2 + 5;
        rectTransform.sizeDelta     = Vector3.one * size;
    }
}
