using UnityEngine;

public class Wood : Solid
{
    public Wood() : base(1500, RoomTemperature, CreateRandomWoodColor(), Elements.woodId)
    {
    }

    private static Color32 CreateRandomWoodColor()
    {
        byte red = (byte)Random.Range(130, 160);
        byte green = (byte)Random.Range(35, 95);
        byte blue = (byte)Random.Range(35, 65);

        return new Color32(red, green, blue, 255);
    }
}
