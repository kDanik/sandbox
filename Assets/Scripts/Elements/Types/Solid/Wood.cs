using UnityEngine;

public class Wood : Solid
{
    public Wood() : this(CreateRandomWoodColor())
    {
    }

    public Wood(Color32 color) : base(1500, RoomTemperature, color, Elements.woodId)
    {
        heatReactionTemperature = 644;
    }

    private static Color32 CreateRandomWoodColor()
    {
        byte red = (byte)Random.Range(130, 160);
        byte green = (byte)Random.Range(35, 95);
        byte blue = (byte)Random.Range(35, 65);

        return new Color32(red, green, blue, 255);
    }

    public override void HeatReaction(BaseElement elementWithHigherTempreture, ElementGrid elementGrid)
    {
        elementGrid.SetElement(x, y, new BurningWood());
    }
}
