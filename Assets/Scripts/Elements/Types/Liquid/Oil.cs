using UnityEngine;

public class Oil : Liquid
{
    public Oil() : base(750, RoomTemperature, CreateRandomOilColor())
    {
    }

    private static Color CreateRandomOilColor()
    {
        float red = Random.Range(0.5f, 1f);
        float green = Random.Range(0.5f, 1f);
        float blue = Random.Range(0.0f, 0.01f);
        float alpha = 0.5f;

        return new Color(red, green, blue, alpha);
    }
}
