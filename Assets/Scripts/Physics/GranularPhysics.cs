using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranularPhysics
{
    private static readonly bool directionLeft = true;

    // private static readonly bool directionRight = false;

    private ElementGrid elementGrid;

    private bool direction = true;
    public GranularPhysics(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;
    }

    public void Simulate(int x, int y)
    {

        if (TrySwapWithBottomElement(x, y))
        {   
            y--;
            if (TrySwapWithBottomElement(x, y))
            {
                y--;
            }
        }

        if (IsSwappableElement(x, y - 1)) return;


        if (direction.Equals(directionLeft))
        {
            SwitchDirection();

            if (TrySwapWithBottomLeftElement(x, y) && TrySwapWithBottomLeftElement(x - 1, y - 1)) return;

            if (TrySwapWithBottomRightElement(x, y)) return;
        }
        else
        {
            SwitchDirection();

            if (TrySwapWithBottomRightElement(x, y) && TrySwapWithBottomRightElement(x + 1, y - 1)) return;

            if (TrySwapWithBottomLeftElement(x, y)) return;
        }
    }

    private void SwitchDirection()
    {
        direction = !direction;
    }

    private bool TrySwapWithBottomElement(int x, int y)
    {
        if (!IsSwappableElement(x, y - 1)) return false;

        elementGrid.SwapElements(x, y, x, y - 1);

        return true;
    }

    private bool TrySwapWithBottomLeftElement(int x, int y)
    {
        if (!IsSwappableElement(x - 1, y - 1) || !IsSwappableElement(x - 1, y)) return false;

        elementGrid.SwapElements(x, y, x - 1, y - 1);

        return true;
    }

    private bool TrySwapWithBottomRightElement(int x, int y)
    {
        if (!IsSwappableElement(x + 1, y - 1) || !IsSwappableElement(x + 1, y)) return false;

        elementGrid.SwapElements(x, y, x + 1, y - 1);

        return true;
    }

    // for granular material swappable elements would be liquids and gasses or empty space
    private bool IsSwappableElement(int x, int y)
    {
        return elementGrid.IsInBounds(x, y) && (elementGrid.GetElement(x, y) == null || elementGrid.GetElement(x, y) is Liquid);
    }

    private bool isLiquidElement(int x, int y)
    {
        return elementGrid.GetElement(x, y) is Liquid;
    }
}