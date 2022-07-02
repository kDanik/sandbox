using UnityEngine;

public class Stone : Solid
{
    public Stone() : base(3000, RoomTemperature, CreateRandomStoneColor())
    {
    }

    private static Color32 CreateRandomStoneColor()
    {
        byte allColorValues = (byte)Random.Range(100, 200);

        return new Color32(allColorValues, allColorValues, allColorValues, 255);
    }
}
