using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GranularPhysics : AbstractElementPhysics
{
    // true value is for left direction, false is for right direction
    private static readonly bool directionLeft = true;

    // direction to check first for physics calculation.
    // Exists to prevent physics calculation favoring one direction
    private bool direction = true;

    public GranularPhysics(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;
    }

    public override void Simulate(BaseElement granularElement)
    {
        int x = granularElement.x;
        int y = granularElement.y;

        TrySwapWithBottomElement(ref x, ref y, granularElement);

        TrySwapWithBottomElement(ref x, ref y, granularElement);


        if (GetElementSwapInfo(x, y - 1, granularElement).isSwappable) return;


        // direction decides which side (left or right) will be checked first for physics calculation.
        if (direction.Equals(directionLeft))
        {
            SwitchDirection();

            if (TrySwapWithBottomLeftElement(ref x,ref y, granularElement) && TrySwapWithBottomLeftElement(ref x, ref y, granularElement)) return;

            if (TrySwapWithBottomRightElement(ref x, ref y, granularElement)) return;
        }
        else
        {
            SwitchDirection();

            if (TrySwapWithBottomRightElement(ref x,ref y, granularElement) && TrySwapWithBottomRightElement(ref x, ref y, granularElement)) return;

            if (TrySwapWithBottomLeftElement(ref x, ref y, granularElement)) return;
        }
    }


    // Switches direction to oposite value
    private void SwitchDirection()
    {
        direction = !direction;
    }

    // Tries to swap with bottom element
    // on success updates position (x, y) and returns true
    private bool TrySwapWithBottomElement(ref int x, ref int y, BaseElement granular)
    {
        ElementSwapInfo bottomElementInfo = GetElementSwapInfo(x, y - 1, granular);

        if (!bottomElementInfo.isSwappable) return false;


        // tries to move liquid or gas to the side instead of swapping with it
        // makes granular materials that fall on liquids or gas act a little more natural
        if (bottomElementInfo.isLiquid || bottomElementInfo.isGas)
        {
            if (direction.Equals(directionLeft))
            {
                if (elementGrid.IsInBoundsAndEmpty(x + 1, y))
                {
                    elementGrid.SwapElements(x, y - 1, x + 1, y);
                }
                else
                {
                    if (elementGrid.IsInBoundsAndEmpty(x - 1, y))
                    {
                        elementGrid.SwapElements(x, y - 1, x - 1, y);
                    }
                }
            }
            else
            {
                if (elementGrid.IsInBoundsAndEmpty(x + 1, y))
                {
                    elementGrid.SwapElements(x, y - 1, x + 1, y);
                }
                else
                {
                    if (elementGrid.IsInBoundsAndEmpty(x - 1, y))
                    {
                        elementGrid.SwapElements(x, y - 1, x - 1, y);
                    }
                }
            }
        }

        elementGrid.SwapElements(x, y, x, y - 1);

        y--;

        return true;
    }




    // Tries to swap with bottom left element
    // on success updates position (x, y) and returns true
    private bool TrySwapWithBottomLeftElement(ref int x, ref int y, BaseElement granular)
    {
        ElementSwapInfo bottomLeftElementInfo = GetElementSwapInfo(x - 1, y - 1, granular);
        ElementSwapInfo leftElementInfo = GetElementSwapInfo(x - 1, y, granular);

        if (!bottomLeftElementInfo.isSwappable || !leftElementInfo.isSwappable) return false;

        // tries to move liquid or gas to the side instead of swapping with it
        // makes granular materials that fall on liquids or gas act a little more natural
        if (bottomLeftElementInfo.isLiquid || bottomLeftElementInfo.isGas)
        {

            if (leftElementInfo.isLiquid || leftElementInfo.isGas)
            {
                // swaps liquid element (bottom left) with empty space (left, top)
                elementGrid.SwapElements(x - 1, y - 1, x - 1, y);
            }
            else
            {
                if (elementGrid.IsInBoundsAndEmpty(x - 2, y))
                {
                    // swaps liquid element (bottom left) with empty space (left twice, top)
                    elementGrid.SwapElements(x - 1, y - 1, x - 2, y);
                }
            }
        }


        elementGrid.SwapElements(x, y, x - 1, y - 1);
        y--;
        x--;

        return true;
    }

    // Tries to swap with bottom right element
    // on success updates position (x, y) and returns true
    private bool TrySwapWithBottomRightElement(ref int x, ref int y, BaseElement granular)
    {
        ElementSwapInfo bottomRightElementInfo = GetElementSwapInfo(x + 1, y - 1, granular);
        ElementSwapInfo rightElementInfo = GetElementSwapInfo(x + 1, y, granular);

        if (!bottomRightElementInfo.isSwappable || !rightElementInfo.isSwappable) return false;


        // tries to move liquid or gas to the side instead of swapping with it
        // makes granular materials that fall on liquids or gas act a little more natural
        if (bottomRightElementInfo.isLiquid || bottomRightElementInfo.isGas)
        {

            if (rightElementInfo.isLiquid || rightElementInfo.isGas)
            {
                // swaps liquid element (bottom right) with empty space (right, top)
                elementGrid.SwapElements(x + 1, y - 1, x + 1, y);
            }
            else
            {
                if (elementGrid.IsInBoundsAndEmpty(x + 2, y))
                {
                    // swaps liquid element (bottom right) with empty space (right twice, top)
                    elementGrid.SwapElements(x + 1, y - 1, x + 2, y);
                }
            }
        }

        elementGrid.SwapElements(x, y, x + 1, y - 1);
        y--;
        x++;

        return true;
    }
}