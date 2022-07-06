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



        // TODO Can this be replaced by object nicely and simply ?


        // fire - methane
        if (centerElement.elementTypeId == Elements.fireId && adjacentElement.elementTypeId == Elements.methaneId)
        {
            ReactionFireWithMethane(adjacentElement);
            return true;
        } 

        if (centerElement.elementTypeId == Elements.methaneId && adjacentElement.elementTypeId == Elements.fireId)
        {
            ReactionFireWithMethane(centerElement);
            return true;
        }

        // fire - oil
        if (centerElement.elementTypeId == Elements.fireId && adjacentElement.elementTypeId == Elements.oilId)
        {
            ReactionFireWithOil(adjacentElement);
            return true;
        }

        if (centerElement.elementTypeId == Elements.oilId && adjacentElement.elementTypeId == Elements.fireId)
        {
            ReactionFireWithOil(centerElement);
            return true;
        }


        // fire - wood
        if (centerElement.elementTypeId == Elements.fireId && adjacentElement.elementTypeId == Elements.woodId)
        {
            ReactionFireWithWood(adjacentElement);
            return true;
        }

        if (centerElement.elementTypeId == Elements.woodId && adjacentElement.elementTypeId == Elements.fireId)
        {
            ReactionFireWithWood(centerElement);
            return true;
        }

        return false;
    }

    private void ReactionFireWithMethane(BaseElement methane) {
        elementGrid.SetElement(methane.x, methane.y, new Fire());
    }

    private void ReactionFireWithOil(BaseElement oil)
    {
        elementGrid.SetElement(oil.x, oil.y, new BurningOil());
    }

    private void ReactionFireWithWood(BaseElement wood)
    {
        elementGrid.SetElement(wood.x, wood.y, new BurningWood());
    }

    private void ReactionFireWithWater(BaseElement fire, BaseElement water)
    {
        elementGrid.SetElement(fire.x, fire.y, new BurningWood());
    }
}
