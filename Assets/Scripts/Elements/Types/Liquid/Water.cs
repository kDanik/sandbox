    using UnityEngine;

public class Water : Liquid
{
    public Water() : base(1000, RoomTemperature, CreateRandomWaterColor(), Elements.waterId)
    {
        heatReactionTemperature = 373;
    }

    private static Color32 CreateRandomWaterColor()
    {
        byte red = (byte)Random.Range(0, 15);
        byte green = (byte)Random.Range(0, 25);
        byte blue = (byte)Random.Range(210, 255);

        return new Color32(red, green, blue, 125);
    }

    public override void HeatReaction(BaseElement elementWithHigherTemperature, ElementGrid elementGrid)
    {
        elementGrid.SetElement(x, y, new Steam());

        if (elementWithHigherTemperature.elementTypeId.Equals(Elements.fireId))
        {
            elementGrid.SetElement(elementWithHigherTemperature.x, elementWithHigherTemperature.y, null);
            return;
        }

        if (elementWithHigherTemperature.elementTypeId.Equals(Elements.meltedGlassId))
        {
            if (Random.Range(1, 5) == 1) elementGrid.SetElement(elementWithHigherTemperature.x, elementWithHigherTemperature.y, new Glass());
            return;
        }

        if (elementWithHigherTemperature.elementTypeId.Equals(Elements.burningWoodId))
        {
            Color32 newWoodColor = ColorUtil.GetDarkerColor(elementWithHigherTemperature.GetColor(), 2);

            elementGrid.SetElement(elementWithHigherTemperature.x, elementWithHigherTemperature.y, new Wood(newWoodColor));
            return;
        }

        if (elementWithHigherTemperature.elementTypeId.Equals(Elements.burningOilId))
        {
            elementGrid.SetElement(elementWithHigherTemperature.x, elementWithHigherTemperature.y, new Fire());
            return;
        }
    }
}
