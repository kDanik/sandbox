using UnityEngine;

public class Ash : GranularMaterial
{
    public Ash() : base(1540, RoomTemperature, CreateRandomAshColor(), Elements.ashId)
    {
    }

    private static Color32 CreateRandomAshColor()
    {
        byte allColorValues = (byte)Random.Range(190, 230);

        return new Color32(allColorValues, allColorValues, allColorValues, 255);
    }
}
