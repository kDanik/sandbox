    using UnityEngine;

public class Water : Liquid
{
    public Water() : base(1000, RoomTemperature, CreateRandomWaterColor(), Elements.waterId)
    {
    }

    private static Color32 CreateRandomWaterColor()
    {
        byte red = (byte)Random.Range(0, 15);
        byte green = (byte)Random.Range(0, 25);
        byte blue = (byte)Random.Range(210, 255);

        return new Color32(red, green, blue, 125);
    }
}
