using UnityEngine;

public class Steam : Gas
{
    public Steam() : base(10f, 1900, CreateRandomSteamColor(), Elements.steamId)
    {
    }

    private static Color32 CreateRandomSteamColor()
    {
        byte allColorValues = (byte)Random.Range(190, 240);

        return new Color32(allColorValues, allColorValues, allColorValues, 190);
    }

    public override void TimedAction(ElementGrid elementGrid)
    {
        elementGrid.SetElement(x, y, new Water());
    }
}
