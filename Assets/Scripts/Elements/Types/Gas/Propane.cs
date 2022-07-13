using UnityEngine;

public class Propane : Gas
{
    private const uint PropaneBurnTemperature = 2223;

    public Propane() : base(44.1f, RoomTemperature, CreateRandomPropaneColor(), Elements.propaneId)
    {
        heatReactionTemperature = 723;
    }

    private static Color32 CreateRandomPropaneColor()
    {
        byte red = (byte)Random.Range(25, 45);
        byte green = (byte)Random.Range(25, 45);

        return new Color32(red, green, 0, 90);
    }

    public override void HeatReaction(BaseElement elementWithHigherTemperature, ElementGrid elementGrid)
    {
        elementGrid.SetElement(x, y, new Fire(temperature: PropaneBurnTemperature, durationLimit: 30, noSmoke: true));

        elementGrid.SetElementIfEmpty(x, y + 1, new Fire(temperature: PropaneBurnTemperature, durationLimit: 30, noSmoke: true));

        elementGrid.SetElementIfEmpty(x, y - 1, new Fire(temperature: PropaneBurnTemperature, durationLimit: 30, noSmoke: true));

        elementGrid.SetElementIfEmpty(x + 1, y, new Fire(temperature: PropaneBurnTemperature, durationLimit: 30, noSmoke: true));

        elementGrid.SetElementIfEmpty(x - 1, y, new Fire(temperature: PropaneBurnTemperature, durationLimit: 30, noSmoke: true));
    }
}
