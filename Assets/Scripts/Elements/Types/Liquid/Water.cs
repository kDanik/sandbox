using UnityEngine;

public class Water : Liquid
{
    public Water() : base(1000, RoomTemperature, CreateRandomSandColor())
    {
    }

    private static Color CreateRandomSandColor()
    {
        float red = Random.Range(0.0f, 0.05f);
        float green = Random.Range(0.0f, 0.1f);
        float blue = Random.Range(0.8f, 1f);
        float alpha = 0.5f;

        return new Color(red, green, blue, alpha);
    }
}
