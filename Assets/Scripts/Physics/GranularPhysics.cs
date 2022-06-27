using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranularPhysics
{
    private ElementGrid elementGrid;

    // true value is left direction, false is right direction
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


        if (IsSwappableElement(x, y - 1)) return;


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

    private void SwitchDirection()
    {
        direction = !direction;
    }

    // Tries to swap with bottom element
    // on success updates position (x, y) and returns true
    private bool TrySwapWithBottomElement(ref int x, ref int y)
    {
        if (!IsSwappableElement(x, y - 1)) return false;

        elementGrid.SwapElements(x, y, x, y - 1);
        y--;

        return true;
    }

    // Tries to swap with bottom left element
    // on success updates position (x, y) and returns true
    private bool TrySwapWithBottomLeftElement(ref int x, ref int y)
    {
        if (!IsSwappableElement(x - 1, y - 1) || !IsSwappableElement(x - 1, y)) return false;

        elementGrid.SwapElements(x, y, x - 1, y - 1);
        y--;
        x--;

        return true;
    }

    // Tries to swap with bottom right element
    // on success updates position (x, y) and returns true
    private bool TrySwapWithBottomRightElement(ref int x, ref int y)
    {
        if (!IsSwappableElement(x + 1, y - 1) || !IsSwappableElement(x + 1, y)) return false;

        elementGrid.SwapElements(x, y, x + 1, y - 1);
        y--;
        x++;

        return true;
    }

    // for granular material swappable elements would be liquids and gasses or empty space
    private bool IsSwappableElement(int x, int y)
    {
        return elementGrid.IsInBounds(x, y) && (elementGrid.GetElement(x, y) == null || elementGrid.GetElement(x, y) is Liquid);
    }
}