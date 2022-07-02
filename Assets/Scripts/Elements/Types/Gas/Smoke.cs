using UnityEngine;

public class Smoke : Gas
{
    public Smoke() : base(10.1f, RoomTemperature, CreateRandomSmokeColor())
    {
    }

    private static Color32 CreateRandomSmokeColor()
    {
        byte allColorValues = (byte)Random.Range(0, 105);

        return new Color32(allColorValues, allColorValues, allColorValues, 190);
    }
}
