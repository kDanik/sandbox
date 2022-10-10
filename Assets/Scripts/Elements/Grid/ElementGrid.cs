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
    public bool[,] checkThisIteration;


    // positions that were already checked by physics calculations
    // and should be ignored this iteration
    private bool[,] ignoreThisIteration;


    // pixel renderer used to updated colors of elements when they are created, deleted, moved
    private readonly SandboxPixelRenderer sandboxPixelRenderer;

    // initial height and width of grid
    private readonly int height;
    private readonly int width;

    // border width
    private readonly int borderWidth = 2;


    public ElementGrid(int width, int height, SandboxPixelRenderer sandboxPixelRenderer)
    {
        this.height = height;
        this.width = width;
        this.sandboxPixelRenderer = sandboxPixelRenderer;

        elementGrid = new BaseElement[width, height];

        ignoreThisIteration = new bool[width, height];
        checkNextIteration = new bool[width, height];

        CreateGridBorder();
    }

    private void CreateGridBorder() {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (!IsInBounds(x, y))
                {
                    elementGrid[x, y] = new Border();
                    UpdateColorValues(x, y, elementGrid[x, y]);
                }
            }
        }
    }

    /// <summary>
    /// Copies current checkNextIteration array to checkThisIteration and clears checkNextIteration array
    /// </summary>
    public void PrepareCheckThisIteration()
    {
        checkThisIteration = checkNextIteration;

        checkNextIteration = new bool[width, height];
    }

    /// <returns>
    /// returns true if position is in ignoreThisIteration array and should be ignored for calculation this iteration
    /// </returns>
    public bool IsIgnorePosition(int x, int y)
    {
        return ignoreThisIteration[x, y] == true;
    }

    /// <summary>
    /// Unused! checkThisIteration[x, y] should be accesssed directly for MUCH better performance
    /// </summary>
    /// <returns>
    /// returns true if position is in checkThisIteration array and should be used for physics/reactions calculations this frame
    /// </returns>
    public bool IsPositionToCheckInCurrentIteration(int x, int y)
    {
        return checkThisIteration[x, y] == true;
    }

    /// <returns>
    /// creates new array for ignoreThisIteration instead existing one
    /// </returns>
    public void ClearIgnoreThisIterationPositionsList()
    {
        ignoreThisIteration = new bool[width, height];
    }

    /// <summary>
    /// swaps 2 elements if both are in bound. Also works if one of elements is null
    /// </summary>
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

            if (elementGrid[x2, y2] != null)
            {
                elementGrid[x2, y2].x = x2;
                elementGrid[x2, y2].y = y2;
            }


            if (elementGrid[x1, y1] != null)
            {
                elementGrid[x1, y1].x = x1;
                elementGrid[x1, y1].y = y1;
            }


            UpdateColorValues(x2, y2, elementGrid[x2, y2]);
            UpdateColorValues(x1, y1, elementGrid[x1, y1]);
        }
    }

    /// <returns>
    /// returns element for given position if in bounds, otherwise returns null
    /// </returns>
    public BaseElement GetElement(int x, int y)
    {
        if (IsInBoundsIncludingBorder(x, y))
        {
            return elementGrid[x, y];
        }
        else
        {
            return null;
        }
    }

    /// <returns>
    /// UNSAFE version of GetElement. Will NOT check if position is in bounds. Should be ONLY used if position are checked before.
    /// Unsafe get is much more performant than normal get.
    /// Returns element for given position,
    /// </returns>
    public BaseElement GetElementUnsafe(int x, int y)
    {
        return elementGrid[x, y];
    }


    /// <summary>
    /// if position is in bounds and no other element is assigned for it,  sets element and updates pixel color for it.
    /// Returns true if element was set successfully
    /// </summary>
    public bool SetElementIfEmpty(int x, int y, BaseElement element)
    {
        if (!IsInBounds(x, y) || elementGrid[x, y] != null) return false;


        if (element != null)
        {
            element.x = x;
            element.y = y;
        }

        elementGrid[x, y] = element;

        ignoreThisIteration[x, y] = true;
        AddSuroundingPositionsToCheckNextIteration(x, y);

        UpdateColorValues(x, y, element);

        return true;

    }

    /// <summary>
    /// if position is in bounds, sets element and updates pixel color for it,
    /// </summary>
    public void SetElement(int x, int y, BaseElement element)
    {
        if (!IsInBounds(x, y)) return;

        if (element != null)
        {
            element.x = x;
            element.y = y;
        }

        if (elementGrid[x, y] != null) elementGrid[x, y].Destroy(this);

        elementGrid[x, y] = element;


        ignoreThisIteration[x, y] = true;
        AddSuroundingPositionsToCheckNextIteration(x, y);

        UpdateColorValues(x, y, element);
        
    }

    /// <summary>
    /// Updates sandboxPixelRenderer pixel for given position. ApplyCurrentChangesToTexture() should be used to appply changes to texture
    /// </summary>
    public void UpdateColorValues(int x, int y, BaseElement element)
    {
        if (element != null)
        {
            sandboxPixelRenderer.SetPixel(x, y, element.GetColor());
        }
        else
        {
            sandboxPixelRenderer.SetPixel(x, y, new Color32(0, 0, 0, 0));
        }
    }


    /// <returns>
    /// Returns true if position is in bounds of the grid and there is no element present
    /// </returns>
    public bool IsInBoundsAndEmpty(int x, int y)
    {
        return x >= borderWidth && y >= borderWidth && x < width - borderWidth && y < height - borderWidth && elementGrid[x, y] == null;
    }

    /// <summary>
    /// Adds all surounding positions to positionsChangedThisIteration, so their physics calculated next iteration
    /// </summary>
    public void AddSuroundingPositionsToCheckNextIteration(int x, int y)
    {
        // center
        checkNextIteration[x, y] = true;

        if (y > borderWidth)
        {
            // down
            checkNextIteration[x, y - 1] = true;

            // right down
            if (x + 1 < width - borderWidth) checkNextIteration[x + 1, y - 1] = true;

            // left bottom
            if (x > borderWidth) checkNextIteration[x - 1, y - 1] = true;
        }

        if (y + 1 < height - borderWidth)
        {
            // top
            checkNextIteration[x, y + 1] = true;

            // right top
            if (x + 1 < width - borderWidth) checkNextIteration[x + 1, y + 1] = true;

            // left top
            if (x > borderWidth) checkNextIteration[x - 1, y + 1] = true;
        }


        // left
        if (x > borderWidth) checkNextIteration[x - 1, y] = true;
        // right
        if (x + 1 < width - borderWidth) checkNextIteration[x + 1, y] = true;
    }

    /// <summary>
    /// Adds one position to positionsChangedThisIteration, so it's physics calculated next iteration
    /// </summary>
    public void AddPositionToCheckNextIteration(int x, int y)
    {
        checkNextIteration[x, y] = true;
    }

    /// <returns>
    /// Returns true if position is in bounds of the grid (excluding border)
    /// </returns>
    public bool IsInBounds(int x, int y)
    {
        return x >= borderWidth && y >= borderWidth && x < width - borderWidth && y < height - borderWidth; 
    }

    /// <returns>
    /// Returns true if position is in bounds of the grid (including border)
    /// </returns>
    public bool IsInBoundsIncludingBorder(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    /// <returns>
    /// Returns true if position is in bounds of the grid and there is element present
    /// </returns>
    public bool ElementPresent(int x, int y)
    {
        return IsInBounds(x, y) && elementGrid[x, y] != null;
    }

    public int GetHeight()
    {
        return height;
    }

    public int GetWidth()
    {
        return width;
    }
}
