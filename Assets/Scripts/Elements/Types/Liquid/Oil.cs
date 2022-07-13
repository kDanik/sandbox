using UnityEngine;

public class Oil : Liquid
{
    public Oil() : base(750, RoomTemperature, CreateRandomOilColor(), Elements.oilId)
    {
        heatReactionTemperature = 600;
    }

    private static Color32 CreateRandomOilColor()
    {   
        byte red = (byte)Random.Range(200, 255);
        byte green = (byte)Random.Range(200, 255);

        return new Color32(red, green, 0, 80);
    }

    public override void HeatReaction(BaseElement elementWithHigherTemperature, ElementGrid elementGrid)
    {
        elementGrid.SetElement(x, y, new BurningOil());
    }
}
