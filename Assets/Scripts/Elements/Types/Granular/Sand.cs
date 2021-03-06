using UnityEngine;

public class Sand : GranularMaterial
{
    public Sand() : base(1540, RoomTemperature, CreateRandomSandColor(), Elements.sandId)
    {
        heatReactionTemperature = 1227;
    }

    private static Color32 CreateRandomSandColor()
    {
        byte red = (byte)Random.Range(190, 255);
        byte green = (byte)Random.Range(190, 255);
        byte blue = (byte)Random.Range(110, 160);

        return new Color32(red, green, blue, 255);
    }

    public override void HeatReaction(BaseElement elementWithHigherTemperature, ElementGrid elementGrid)
    {
        elementGrid.SetElement(x, y, new MeltedGlass());
    }
}
