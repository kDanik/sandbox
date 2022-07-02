using UnityEngine;

public class Methane : Gas
{
    public Methane() : base(16.04f, RoomTemperature, CreateRandomMethaneColor())
    {
    }

    private static Color32 CreateRandomMethaneColor()
    {
        byte red = (byte)Random.Range(190, 255);
        byte green = (byte)Random.Range(25, 45);
        byte blue = (byte)Random.Range(25, 45);

        return new Color32(red, green, blue, 90);
    }
}
