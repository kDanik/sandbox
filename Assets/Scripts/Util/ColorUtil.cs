using UnityEngine;
public static class ColorUtil
{
    /// <summary>
    /// Makes give color darker depending on given multiplier.
    /// Example: 2.0f multiplier will make color 2 times darker
    /// </summary>
    public static Color32 GetDarkerColor(Color32 initialColor, float multiplier) {
        initialColor.r = (byte)(initialColor.a / multiplier);
        initialColor.g = (byte)(initialColor.g / multiplier);
        initialColor.b = (byte)(initialColor.b / multiplier);

        return initialColor;
    }
}
