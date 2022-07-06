using UnityEngine;
public class Fire : Gas
{
    public Fire() : base(10f, 1900, CreateRandomFireColor(), Elements.fireId)
    {
        TimedActions.AddTimedAction((uint)Random.Range(30, 50), this);
    }

    private static Color32 CreateRandomFireColor()
    {
        byte red = (byte)Random.Range(210, 255);
        byte green = (byte)Random.Range(50, 105);

        return new Color32(red, green, 0, 200);
    }

    public override void TimedAction(ElementGrid elementGrid)
    {
        if (Random.Range(1, 5) == 1)
        {
            elementGrid.SetElement(x, y, new Smoke());
        }
        else
        {
            elementGrid.SetElement(x, y, null);
        }
    }
}
