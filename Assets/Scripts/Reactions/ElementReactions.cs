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


        // fire - water
        if (centerElement.elementTypeId == Elements.fireId && adjacentElement.elementTypeId == Elements.waterId)
        {
            ReactionFireWithWater(centerElement, adjacentElement);
            return true;
        }

        if (centerElement.elementTypeId == Elements.waterId && adjacentElement.elementTypeId == Elements.fireId)
        {
            ReactionFireWithWater(adjacentElement, centerElement);
            return true;
        }

        // steam - solid 
        if (centerElement.elementTypeId == Elements.steamId && adjacentElement.IsSolid())
        {
            ReactionSteamWithSolid(centerElement);
            return true;
        }

        // water - burning wood 
        if (centerElement.elementTypeId == Elements.waterId && adjacentElement.elementTypeId == Elements.burningWoodId)
        {
            ReactionBurningWoodWithWater(adjacentElement, centerElement);
            return true;
        }

        if (centerElement.elementTypeId == Elements.burningWoodId && adjacentElement.elementTypeId == Elements.waterId)
        {
            ReactionBurningWoodWithWater(centerElement, adjacentElement);
            return true;
        }

        // fuse - fire

        if (adjacentElement.elementTypeId == Elements.fuseId && centerElement.elementTypeId == Elements.fireId)
        {
            ReactionFireWithFuse(adjacentElement);
            return true;
        }

        // glass - fire

        if (adjacentElement.elementTypeId == Elements.fireId && centerElement.elementTypeId == Elements.glassId)
        {
            ReactionFireWithGlass(adjacentElement);
            return true;
        }


        // melted glass - water

        if (adjacentElement.elementTypeId == Elements.meltedGlassId && centerElement.elementTypeId == Elements.waterId)
        {
            ReactionWaterWithMeltedGlass(adjacentElement, centerElement);
            return true;
        }



        // sand - fire 

        if (adjacentElement.elementTypeId == Elements.sandId && centerElement.elementTypeId == Elements.fireId)
        {
            ReactionSandWithFire(adjacentElement);
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
        elementGrid.SetElement(fire.x, fire.y, null);
        elementGrid.SetElement(water.x, water.y, new Steam());
    }

    private void ReactionBurningWoodWithWater(BaseElement burningWood, BaseElement water)
    {
        Color32 newWoodColor = ColorUtil.GetDarkerColor(burningWood.GetColor(), 2);

        elementGrid.SetElement(burningWood.x, burningWood.y, new Wood(newWoodColor));
        elementGrid.SetElement(water.x, water.y, new Steam());
    }

    private void ReactionSteamWithSolid(BaseElement steam)
    {
        TimedActions.AddTimedAction((uint)Random.Range(4, 10), steam);
    }

    private void ReactionFireWithFuse(BaseElement fuse)
    {
        elementGrid.SetElement(fuse.x, fuse.y, new Fire());
    }

    private void ReactionFireWithGlass(BaseElement glass)
    {
        elementGrid.SetElement(glass.x, glass.y, new MeltedGlass());
    }

    private void ReactionWaterWithMeltedGlass(BaseElement meltedGlass, BaseElement water)
    {
        elementGrid.SetElement(water.x, water.y, new Steam());

        if (Random.Range(1, 5) == 1) elementGrid.SetElement(meltedGlass.x, meltedGlass.y, new Glass());
    }

    private void ReactionSandWithFire(BaseElement sand)
    {
        elementGrid.SetElement(sand.x, sand.y, new MeltedGlass());
    }
}
