using UnityEngine;
public class Void : Solid
{


    public Void() : base(3000, RoomTemperature, CreateVoidColor(), Elements.voidId)
    {
    }

    private static Color32 CreateVoidColor()
    {
        return new Color32(68, 35, 71, 255);
    }
}
