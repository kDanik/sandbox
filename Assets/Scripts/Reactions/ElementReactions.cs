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

        if (CheckReactionWithAdjacentElement(centerElement, x + 1, y)) return;

        if (CheckReactionWithAdjacentElement(centerElement, x - 1, y)) return;

        if (CheckReactionWithAdjacentElement(centerElement, x, y + 1)) return;

        if (CheckReactionWithAdjacentElement(centerElement, x, y - 1)) return;
    }


    // checks reaction of 2 elements, if one present and was successfully executed return true, otherwise false.
    // if false returned other reactions should be checked for current center element
    // (sometimes reactions could return false on success too)
    private bool CheckReactionWithAdjacentElement(BaseElement centerElement, int xAdjacent, int yAdjacent)
    {
  
        BaseElement adjacentElement = elementGrid.GetElement(xAdjacent, yAdjacent);

        if (adjacentElement == null) return false;

        switch (centerElement.elementTypeId) {
            case Elements.fireId:
                return CheckReactionWithFire(centerElement,adjacentElement);
            default:
                return false;
        }
    }

    private bool CheckReactionWithFire(BaseElement centerElement, BaseElement adjacentElement)
    {
        switch (adjacentElement.elementTypeId)
        {
            case Elements.methaneId:
                elementGrid.SetElement(adjacentElement.x, adjacentElement.y, new Fire());
                return true;
            default:
                return false;
        }
    }
}
