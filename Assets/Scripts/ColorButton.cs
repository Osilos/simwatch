using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : Button
{
    private Transform center;

    protected override void Awake()
    {
        center = transform.Find("center");
    }

    public virtual IEnumerator Blink(int blinkId, float blinkSecondsDuration)
    {
        transform.SetSiblingIndex(transform.parent.childCount-1);
        ColorBlock initialColorBlock = colors;
        ColorBlock newColorBlock     = colors;
        newColorBlock.disabledColor  = newColorBlock.normalColor = colors.pressedColor;
        colors                       = newColorBlock;
        Vector3 currentPosition      = transform.position;
        transform.position          -= (center.position - transform.position).normalized * 20;

        yield return new WaitForSeconds(blinkSecondsDuration);

        colors             = initialColorBlock;
        transform.position = currentPosition;
    }
}