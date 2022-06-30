using UnityEngine;

public class Oil : Liquid
{
    public Oil() : base(750, RoomTemperature, CreateRandomOilColor())
    {
    }

    private static Color CreateRandomOilColor()
    {
        float red = Random.Range(0.8f, 1f);
        float green = Random.Range(0.8f, 1f);
        float blue = 0f;
        float alpha = 0.4f;

        return new Color(red, green, blue, alpha);
    }
}
