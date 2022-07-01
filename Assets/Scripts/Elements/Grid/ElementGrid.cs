using System.Collections.Generic;
using UnityEngine;


// IMPORTANT!
// this class contains information about elementGrid and simplest methods to work with its elements
// 
//
// some methods in this class look like they could be simplified by using each other,
// but they are so ugly for performance reasons, to avoid double check
// IMPORTANT!
public class ElementGrid
{
    private readonly BaseElement[,] elementGrid;

    // positions that changed is some form this iteration(or their neighbors), and should be checked next iteration
    // and should be checked by physics class or some other class next iteration
    private bool[,] checkNextIteration;


    // copy of checkNextIteration from previous iteration used for this iteration
    private bool[,] checkThisIteration;


    // positions that were already checked by physics calculations
    // and should be ignored this iteration
    private bool[,] ignoreThisIteration;


    // pixel renderer used to updated colors of elements when they are created, deleted, moved
    private readonly SandboxPixelRenderer sandboxPixelRenderer;

    // initial height and width of grid
    private readonly int height;
    private readonly int width;

    public ElementGrid(int width, int height, SandboxPixelRenderer sandboxPixelRenderer)
    {
        this.height = height;
        this.width = width;
        this.sandboxPixelRenderer = sandboxPixelRenderer;

        elementGrid = new BaseElement[width, height];
        ignoreThisIteration = new bool[width, height];
        checkNextIteration = new bool[width, height];
    }

    // returns positionsChangedThisIteration as array and clears it
    public void PrepareCheckThisIteration()
    {
        checkThisIteration = (bool[,])checkNextIteration.Clone();

        checkNextIteration = new bool[width, height];
    }

    // returns true if position is in ignore array for this iteration
    public bool IsIgnorePosition(int x, int y)
    {
        return ignoreThisIteration[x, y] == true;
    }

    // returns true if position is in check this iteration array
    public bool IsPositionToCheckInCurrentIteration(int x, int y)
    {
        return checkThisIteration[x, y] == true;
    }

    // creates new array for ignoreThisIteration instead existing one
    public void ClearIgnoreThisIterationPositionsList()
    {
        ignoreThisIteration = new bool[width, height];
    }

    // swaps 2 elements if both are in bound. Also works if one of elements is null
    public void SwapElements(int x1, int y1, int x2, int y2)
    {
        if (IsInBounds(x1, y1) && IsInBounds(x2, y2))
        {
            BaseElement buff = elementGrid[x2, y2];
            elementGrid[x2, y2] = elementGrid[x1, y1];
            elementGrid[x1, y1] = buff;

            ignoreThisIteration[x1, y1] = true;
            ignoreThisIteration[x2, y2] = true;
            AddSuroundingPositionsToCheckNextIteration(x1, y1);
            AddSuroundingPositionsToCheckNextIteration(x2, y2);

            UpdateColorValues(x2, y2, elementGrid[x2, y2]);
            UpdateColorValues(x1, y1, elementGrid[x1, y1]);
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

            ignoreThisIteration[x, y] = true;
            AddSuroundingPositionsToCheckNextIteration(x, y);

            UpdateColorValues(x, y, element);
        }
    }

    // if position is in bounds, sets element and updates pixel color for it,
    public void SetElement(int x, int y, BaseElement element)
    {
        if (IsInBounds(x, y))
        {
            elementGrid[x, y] = element;

            ignoreThisIteration[x, y] = true;
            AddSuroundingPositionsToCheckNextIteration(x, y);

            UpdateColorValues(x, y, element);
        }
    }

    // Updates sandboxPixelRenderer pixel for given position
    public void UpdateColorValues(int x, int y, BaseElement element)
    {
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
        checkNextIteration[x, y] = true;

        if (y > 0)
        {
            // down
            checkNextIteration[x, y - 1] = true;

            // right down
            if (x + 1 < height) checkNextIteration[x + 1, y - 1] = true;

            // left bottom
            if (x > 0) checkNextIteration[x - 1, y - 1] = true;
        }

        if (y < height - 1)
        {
            // top
            checkNextIteration[x, y + 1] = true;

            // right top
            if (x + 1 < height) checkNextIteration[x + 1, y + 1] = true;

            // left top
            if (x > 0) checkNextIteration[x - 1, y + 1] = true;
        }


        // left
        if (x > 0) checkNextIteration[x - 1, y] = true;
        // right
        if (x + 1 < height) checkNextIteration[x + 1, y] = true;
    }

    // Adds one position to positionsChangedThisIteration, so it's physics calculated next iteration
    public void AddPositionToCheckNextIteration(int x, int y)
    {
        checkNextIteration[x, y] = true;
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
