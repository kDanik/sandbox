using UnityEngine;
public class Glass : Solid
{
    public Glass() : base(3000, RoomTemperature, CreateGlassColor(), Elements.glassId)
    {
        heatReactionTemperature = 1227;
    }

    // if created from melted glass initial color should be color of melted glass, and with time it should change to normal glass color
    public Glass(MeltedGlass meltedGlass) : base(3000, RoomTemperature, meltedGlass.GetColor(), Elements.glassId)
    {
        TimedActions.AddTimedAction((uint)Random.Range(5, 25), this);
        heatReactionTemperature = 1227;
    }

    private static Color32 CreateGlassColor()
    {
        return new Color32(0, 100, 255, 50);
    }

    public override void TimedAction(ElementGrid elementGrid)
    {
        ChangeElementsColor(elementGrid, CreateGlassColor());
    }

    public override void HeatReaction(BaseElement elementWithHigherTemperature, ElementGrid elementGrid)
    {
        elementGrid.SetElement(x, y, new MeltedGlass());
    }
}
