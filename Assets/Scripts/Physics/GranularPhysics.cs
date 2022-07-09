using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranularPhysics
{
    private ElementGrid elementGrid;

    // true value is for left direction, false is for right direction
    private static readonly bool directionLeft = true;

    // direction to check first for physics calculation.
    // Exists to prevent physics calculation favoring one direction
    private bool direction = true;

    public GranularPhysics(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;
    }

    public void Simulate(int x, int y)
    {
        TrySwapWithBottomElement(ref x, ref y);

        TrySwapWithBottomElement(ref x, ref y);


        if (GetElementInfo(x, y - 1).isSwappable) return;


        // direction decides which side (left or right) will be checked first for physics calculation.
        if (direction.Equals(directionLeft))
        {
            SwitchDirection();

            if (TrySwapWithBottomLeftElement(ref x,ref y) && TrySwapWithBottomLeftElement(ref x, ref y)) return;

            if (TrySwapWithBottomRightElement(ref x, ref y)) return;
        }
        else
        {
            SwitchDirection();

            if (TrySwapWithBottomRightElement(ref x,ref y) && TrySwapWithBottomRightElement(ref x, ref y)) return;

            if (TrySwapWithBottomLeftElement(ref x, ref y)) return;
        }
    }


    // Switches direction to oposite value
    private void SwitchDirection()
    {
        direction = !direction;
    }

    // Tries to swap with bottom element
    // on success updates position (x, y) and returns true
    private bool TrySwapWithBottomElement(ref int x, ref int y)
    {
        ElementInfo bottomElementInfo = GetElementInfo(x, y - 1);

        if (!bottomElementInfo.isSwappable) return false;


        // tries to move liquid or gas to the side instead of swapping with it
        // makes granular materials that fall on liquids or gas act a little more natural
        if (bottomElementInfo.isLiquidOrGas)
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
    private bool TrySwapWithBottomLeftElement(ref int x, ref int y)
    {
        ElementInfo bottomLeftElementInfo = GetElementInfo(x - 1, y - 1);
        ElementInfo leftElementInfo = GetElementInfo(x - 1, y);

        if (!bottomLeftElementInfo.isSwappable || !leftElementInfo.isSwappable) return false;

        // tries to move liquid or gas to the side instead of swapping with it
        // makes granular materials that fall on liquids or gas act a little more natural
        if (bottomLeftElementInfo.isLiquidOrGas)
        {

            if (leftElementInfo.isSwappable && !leftElementInfo.isLiquidOrGas)
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
    private bool TrySwapWithBottomRightElement(ref int x, ref int y)
    {
        ElementInfo bottomRightElementInfo = GetElementInfo(x + 1, y - 1);
        ElementInfo rightElementInfo = GetElementInfo(x + 1, y);

        if (!bottomRightElementInfo.isSwappable || !rightElementInfo.isSwappable) return false;


        // tries to move liquid or gas to the side instead of swapping with it
        // makes granular materials that fall on liquids or gas act a little more natural
        if (bottomRightElementInfo.isLiquidOrGas)
        {
          
            if (rightElementInfo.isSwappable && !rightElementInfo.isLiquidOrGas)
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



    struct ElementInfo
    {
        public bool isSwappable;  // for granular material swappable elements would be liquids and gasses or empty space
        public bool isLiquidOrGas;
    }

    // get ElementInfo for given position
    private ElementInfo GetElementInfo(int x, int y)
    {
        ElementInfo elementInfo;

        // if not in bounds then position is not swappable
        if (!elementGrid.IsInBounds(x, y)) {
            elementInfo.isSwappable = false;
            elementInfo.isLiquidOrGas = false;

            return elementInfo;
        }

        BaseElement elementToSwapWith = elementGrid.GetElementUnsafe(x, y);

        // if in bounds and element is null, then position is free and swapable
        if (elementToSwapWith == null)
        {
            elementInfo.isSwappable = true;
            elementInfo.isLiquidOrGas = false;

            return elementInfo;
        }


        // if in bounds and element is  not null, but is gas or liquid, then position is swappable
        if (elementToSwapWith.IsLiquid() || elementToSwapWith.IsGas())
        {
            elementInfo.isSwappable = true;
            elementInfo.isLiquidOrGas = true;

            return elementInfo;
        }
        else
        {
            elementInfo.isSwappable = false;
            elementInfo.isLiquidOrGas = false;

            return elementInfo;
        }
    }
}