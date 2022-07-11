using UnityEngine;
public class MeltedGlass : Liquid
{
    public MeltedGlass() : base(2200, 1000, CreateMeltedGlassColor(), Elements.meltedGlassId)
    {
        TimedActions.AddTimedAction((uint)Random.Range(60, 90), this);
    }

    private static Color32 CreateMeltedGlassColor()
    {
        return new Color32(255, 70, 0, 150);
    }

    public override void TimedAction(ElementGrid elementGrid)
    {
        elementGrid.SetElement(x, y, new Glass(this));
    }
}
