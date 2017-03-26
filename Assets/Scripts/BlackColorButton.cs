using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlackColorButton : ColorButton {
    protected override void Awake(){}

    public override IEnumerator Blink(int blinkId, float blinkSecondsDuration)
    {
        ColorBlock initialColorBlock = colors;
        ColorBlock newColorBlock = colors;
        newColorBlock.disabledColor = newColorBlock.normalColor = colors.pressedColor;
        colors = newColorBlock;
        Vector3 currentScale = transform.localScale;
        transform.localScale += Vector3.one * 0.2f;

        yield return new WaitForSeconds(blinkSecondsDuration);

        colors = initialColorBlock;
        transform.localScale =  currentScale;
    }
}
