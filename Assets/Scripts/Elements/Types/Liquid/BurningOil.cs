using UnityEngine;
public class BurningOil : Liquid
{
    public BurningOil() : base(700, 500, CreateRandomBurningOilColor(), Elements.burningOilId)
    {
        TimedActions.AddTimedAction((uint)Random.Range(3, 7), this);
    }

    private static Color32 CreateRandomBurningOilColor()
    {
        byte red = (byte)Random.Range(200, 255);
        byte green = (byte)Random.Range(150, 200);

        return new Color32(red, green, 0, 150);
    }

    public override void TimedAction(ElementGrid elementGrid)
    {
        var random = Random.Range(1, 10);
        if (random == 1) {
            elementGrid.SetElement(x, y, new Fire());
            return;
        }

        if (random > 7) {
            elementGrid.SetElementIfEmpty(x, y + 1, new Fire());
        }

        TimedActions.AddTimedAction((uint)Random.Range(5, 20), this);
    }
}