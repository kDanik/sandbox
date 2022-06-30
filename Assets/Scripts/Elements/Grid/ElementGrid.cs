using System.Collections.Generic;
using UnityEngine;


// IMPORTANT!
// this class contains information about elementGrid and simplest methods to work with its elementsw
// 
//
// some methods in this class look like they could be simplified by using each other,
// but they are so ugly for performance reasons, to avoid double check
// IMPORTANT!
public class ElementGrid
{
    private BaseElement[,] elementGrid;

    private SandboxPixelRenderer sandboxPixelRenderer;

    private int height;

    private int width;


    // list with positions that changed is some form this iteration(or their neighbors), and should be checked next iteration
    // and should be checked by physics class or some other class next iteration

    private HashSet<(int x, int y)> checkNextIteration = new HashSet<(int x, int y)>();


    // list with positions that were already checked by physics calculations
    // and should be ignored this iteration
    private HashSet<(int x, int y)> ignoreThisIteration = new HashSet<(int x, int y)>();

    public ElementGrid(int width, int height, SandboxPixelRenderer sandboxPixelRenderer)
    {
        this.height = height;
        this.width = width;
        this.sandboxPixelRenderer = sandboxPixelRenderer;

        elementGrid = new BaseElement[width, height];
    }

    // returns positionsChangedThisIteration as array and clears it
    public HashSet<(int x, int y)> CollectCheckNextIterationPosition()
    {
        var buffer = new HashSet<(int x, int y)>(checkNextIteration);

        checkNextIteration.Clear();

        return buffer;
    }

    // returns true if position is in ignore list for this iteration
    public bool IsIgnorePosition(int x, int y)
    {
        return ignoreThisIteration.Contains((x, y));
    }

    // fully clears list of ignore positions for this iteration
    public void ClearIgnoreThisIterationPositionsList()
    {
        ignoreThisIteration.Clear();
    }

    // swaps 2 elements if both are in bound. Also works if one of elements is null
    public void SwapElements(int x1, int y1, int x2, int y2)
    {
        if (IsInBounds(x1, y1) && IsInBounds(x2, y2))
        {
            BaseElement buff = elementGrid[x2, y2];
            elementGrid[x2, y2] = elementGrid[x1, y1];
            elementGrid[x1, y1] = buff;

            ignoreThisIteration.Add((x1, y1));
            ignoreThisIteration.Add((x2, y2));
            AddSuroundingPositionsToCheckNextIteration(x1, y1);
            AddSuroundingPositionsToCheckNextIteration(x2, y2);

            UpdateColorValues(x2, y2);
            UpdateColorValues(x1, y1);
        }
    }


    // returns element for given position if in bounds, otherwise returns null
    public BaseElement GetElement(int x, int y)
    {
        if (IsInBounds(x, y))
        {
            return elementGrid[x, y];
        }
        else
        {
            return null;
        }
    }

    // if position is in bounds and no other element is assigned for it,  sets element and updates pixel color for it
    public void SetElementIfEmpty(int x, int y, BaseElement element)
    {
        if (!ElementPresent(x, y))
        {
            elementGrid[x, y] = element;

            ignoreThisIteration.Add((x, y));
            AddSuroundingPositionsToCheckNextIteration(x, y);

            UpdateColorValues(x, y);
        }
    }

    // if position is in bounds, sets element and updates pixel color for it,
    public void SetElement(int x, int y, BaseElement element)
    {
        if (IsInBounds(x, y))
        {
            elementGrid[x, y] = element;

            ignoreThisIteration.Add((x, y));
            AddSuroundingPositionsToCheckNextIteration(x, y);

            UpdateColorValues(x, y);
        }
    }

    // Updates sandboxPixelRenderer pixel for given position
    public void UpdateColorValues(int x, int y)
    {

        BaseElement element = GetElement(x, y);
        Color color = Color.clear;

        if (element != null) color = element.color;

        sandboxPixelRenderer.SetPixel(x, y, color);
    }

    // Returns true if position is in bounds of the grid and there is no element present
    public bool IsInBoundsAndEmpty(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height && elementGrid[x, y] == null;
    }


    // Adds all surounding positions to positionsChangedThisIteration, so their physics calculated next iteration
    public void AddSuroundingPositionsToCheckNextIteration(int x, int y)
    {
        // center
        if (ElementPresent(x, y)) checkNextIteration.Add((x, y));
        // bottom
        if (ElementPresent(x, y - 1)) checkNextIteration.Add((x, y - 1));
        // left
        if (ElementPresent(x - 1, y)) checkNextIteration.Add((x - 1, y));
        // up
        if (ElementPresent(x, y + 1)) checkNextIteration.Add((x, y + 1));
        // right
        if (ElementPresent(x + 1, y)) checkNextIteration.Add((x + 1, y));
        // left up
        if (ElementPresent(x - 1, y + 1)) checkNextIteration.Add((x - 1, y + 1));
        // right down
        if (ElementPresent(x + 1, y - 1)) checkNextIteration.Add((x + 1, y - 1));
        // left bottom
        if (ElementPresent(x - 1, y - 1)) checkNextIteration.Add((x - 1, y - 1));
        // top right
        if (ElementPresent(x + 1, y + 1)) checkNextIteration.Add((x + 1, y + 1));
    }

    // Adds one position to positionsChangedThisIteration, so it's physics calculated next iteration
    public void AddPositionToCheckNextIteration(int x, int y)
    {
        checkNextIteration.Add((x, y));
    }

    // Returns true if position is in bounds of the grid
    public bool IsInBounds(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    // Returns true if position is in bounds of the grid and there is element present
    public bool ElementPresent(int x, int y)
    {
        return IsInBounds(x, y) && elementGrid[x, y] != null;
    }
}
