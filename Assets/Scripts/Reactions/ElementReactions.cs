using UnityEngine;
public class ElementReactions
{
    private ElementGrid elementGrid;

    public ElementReactions(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;
    }

    // for given element checks its reactions with top, bottom, left and right elements
    public void CheckReactions(int x, int y, BaseElement centerElement) {

        if (CheckReactionWithAdjacentElement(x, y, centerElement, x + 1, y)) return;

        if (CheckReactionWithAdjacentElement(x, y, centerElement, x - 1, y)) return;

        if (CheckReactionWithAdjacentElement(x, y, centerElement, x, y + 1)) return;

        if (CheckReactionWithAdjacentElement(x, y, centerElement, x, y - 1)) return;
    }


    // checks reaction of 2 elements, if one present and was successfully executed return true, otherwise false.
    // if false returned other reactions should be checked for current center element
    // (sometimes reactions could return false on success too)
    private bool CheckReactionWithAdjacentElement(int xCenter, int yCenter, dynamic centerElement, int xAdjacent, int yAdjacent) {
        try
        {
            dynamic adjacentElement = elementGrid.GetElement(xAdjacent, yAdjacent);

            return adjacentElement != null && CheckReaction(xCenter, yCenter, centerElement, xAdjacent, yAdjacent, adjacentElement);
        }
        catch
        {
            return false;
            // if no method was found for given elements (there is no reaction for them), exception will be thrown
            // maybe there is way to have fallback method instead of try-catch, but i couldn't find it
        }
    }

    // this is test reaction
    private bool CheckReaction(int x1, int y1, Water water, int x2, int y2, BaseElement anyElement)
    {

        if (!(anyElement is Water)) {
            elementGrid.SetElement(x2, y2, null);
            elementGrid.SetElement(x1, y1, null);

            return true;
        }
        return false;
    }
}
