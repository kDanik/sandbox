using UnityEngine;
public class Fire : Gas
{
    private bool noSmoke = false;

    private const uint DefaultFireDurationLimit = 80;

    private const uint DefaultFireTemperature = 1900;

    public Fire(uint durationLimit = DefaultFireDurationLimit, bool noSmoke = false, uint temperature = DefaultFireTemperature) : base(10f, temperature, CreateRandomFireColor(), Elements.fireId)
    {
        TimedActions.AddTimedAction((uint)Random.Range(5, durationLimit), this);
        this.noSmoke = noSmoke;
    }

    public Fire() : this(durationLimit: DefaultFireDurationLimit, noSmoke: false, temperature: DefaultFireTemperature)
    {
    }


    private static Color32 CreateRandomFireColor()
    {
        byte red = (byte)Random.Range(210, 255);
        byte green = (byte)Random.Range(50, 105);

        return new Color32(red, green, 0, 200);
    }

    public override void TimedAction(ElementGrid elementGrid)
    {   if (noSmoke)
        {
            elementGrid.SetElement(x, y, null);
            return;
        }


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
