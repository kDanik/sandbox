
using UnityEngine;

public class Fuse : Solid
{
    public Fuse() : base(3000, RoomTemperature, CreateRandomFuseColor(), Elements.fuseId)
    {
        heatReactionTemperature = 600;
    }

    private static Color32 CreateRandomFuseColor()
    {
        byte allColorValues = (byte)Random.Range(170, 210);

        return new Color32(allColorValues, allColorValues, allColorValues, 255);
    }

    public override void HeatReaction(BaseElement elementWithHigherTempreture, ElementGrid elementGrid)
    {
        elementGrid.SetElement(x, y, new Fire(10, noSmoke:true));
    }
}
