using UnityEngine;
public class Fire : Gas
{
    public Fire() : base(16.04f, 1900, CreateRandomFireColor(), Elements.fireId)
    {
        TimedActions.AddTimedAction((uint)Random.Range(90, 150), this);
    }

    private static Color32 CreateRandomFireColor()
    {
        byte red = (byte)Random.Range(210, 255);
        byte green = (byte)Random.Range(50, 105);

        return new Color32(red, green, 0, 200);
    }

    public override void TimedAction(ElementGrid elementGrid)
    {
        elementGrid.SetElement(this.x, this.y, new Smoke());
    }
}
