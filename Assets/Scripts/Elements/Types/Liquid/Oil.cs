using UnityEngine;

public class Oil : Liquid
{
    public Oil() : base(750, RoomTemperature, CreateRandomOilColor(), Elements.oilId)
    {
    }

    private static Color32 CreateRandomOilColor()
    {   
        byte red = (byte)Random.Range(200, 255);
        byte green = (byte)Random.Range(200, 255);

        return new Color32(red, green, 0, 80);
    }
}
