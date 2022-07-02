using UnityEngine;

public class Propane : Gas
{
    public Propane() : base(44.1f, RoomTemperature, CreateRandomPropaneColor())
    {
    }

    private static Color32 CreateRandomPropaneColor()
    {
        byte red = (byte)Random.Range(25, 45);
        byte green = (byte)Random.Range(25, 45);

        return new Color32(red, green, 0, 90);
    }
}
