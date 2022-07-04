using UnityEngine;

public class Smoke : Gas
{
    public Smoke() : base(10.1f, RoomTemperature, CreateRandomSmokeColor(), Elements.smokeId)
    {
        TimedActions.AddTimedAction((uint)Random.Range(90, 150), this);
    }

    private static Color32 CreateRandomSmokeColor()
    {
        byte allColorValues = (byte)Random.Range(0, 105);

        return new Color32(allColorValues, allColorValues, allColorValues, 190);
    }

    public override void TimedAction(ElementGrid elementGrid)
    {
        elementGrid.SetElement(this.x, this.y, null);
    }
}
