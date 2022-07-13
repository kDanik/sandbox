using UnityEngine;

public class Methane : Gas
{
    private const uint MethaneBurnTemperature = 2223;

    public Methane() : base(16.04f, RoomTemperature, CreateRandomMethaneColor(), Elements.methaneId)
    {
        heatReactionTemperature = 813;
    }

    private static Color32 CreateRandomMethaneColor()
    {
        byte red = (byte)Random.Range(190, 255);
        byte green = (byte)Random.Range(25, 45);
        byte blue = (byte)Random.Range(25, 45);

        return new Color32(red, green, blue, 90);
    }

    public override void HeatReaction(BaseElement elementWithHigherTemperature, ElementGrid elementGrid)
    {
        elementGrid.SetElement(x, y, new Fire(temperature: MethaneBurnTemperature, durationLimit:30, noSmoke: true));

        elementGrid.SetElementIfEmpty(x, y + 1, new Fire(temperature: MethaneBurnTemperature, durationLimit: 30, noSmoke: true));

        elementGrid.SetElementIfEmpty(x, y - 1, new Fire(temperature: MethaneBurnTemperature, durationLimit: 30, noSmoke: true));

        elementGrid.SetElementIfEmpty(x + 1, y, new Fire(temperature: MethaneBurnTemperature, durationLimit: 30, noSmoke: true));

        elementGrid.SetElementIfEmpty(x - 1, y, new Fire(temperature: MethaneBurnTemperature, durationLimit: 30, noSmoke: true));
    }
}
