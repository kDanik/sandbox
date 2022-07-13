using UnityEngine;
public class ElementReactions
{
    private ElementGrid elementGrid;

    public ElementReactions(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;
    }

    // for given element checks its reactions with top, bottom, left and right elements
    public void CheckReactions(int x, int y, BaseElement centerElement)
    {

        if (CheckReactionWithAdjacentElement(centerElement, x + 1, y, DirectionHelper.Right)) return;

        if (CheckReactionWithAdjacentElement(centerElement, x - 1, y, DirectionHelper.Left)) return;

        if (CheckReactionWithAdjacentElement(centerElement, x, y + 1, DirectionHelper.Up)) return;

        if (CheckReactionWithAdjacentElement(centerElement, x, y - 1, DirectionHelper.Down)) return;
    }


    // checks reaction of 2 elements, if one present and was successfully executed return true, otherwise false.
    // if false returned other reactions should be checked for current center element
    // (sometimes reactions could return false on success too)
    private bool CheckReactionWithAdjacentElement(BaseElement centerElement, int xAdjacent, int yAdjacent, DirectionHelper adjacentElementDirection)
    {
        BaseElement adjacentElement = elementGrid.GetElement(xAdjacent, yAdjacent);

        if (adjacentElement == null) return false;

        if (elementGrid.IsIgnorePosition(adjacentElement.x, adjacentElement.y)) return false;


        // heat reactions

        if (centerElement.heatReactionTemperature < adjacentElement.temperature)
        {
            centerElement.HeatReaction(adjacentElement, elementGrid);

            return true;
        }

        if (centerElement.temperature > adjacentElement.heatReactionTemperature)
        {
            adjacentElement.HeatReaction(centerElement, elementGrid);

            return true;
        }

        // freeze reactions

        if (centerElement.freezeReactionTemperature > adjacentElement.temperature)
        {
            centerElement.FreezeReaction(adjacentElement, elementGrid);

            return true;
        }

        if (centerElement.temperature < adjacentElement.freezeReactionTemperature)
        {
            adjacentElement.FreezeReaction(centerElement, elementGrid);

            return true;
        }

        // steam - solid 
        if (centerElement.elementTypeId == Elements.steamId && adjacentElement.IsSolid())
        {
            ReactionSteamWithSolid(centerElement);
            return true;
        }

        return false;
    }

    private void ReactionSteamWithSolid(BaseElement steam)
    {
        TimedActions.AddTimedAction((uint)Random.Range(4, 10), steam);
    }

}
