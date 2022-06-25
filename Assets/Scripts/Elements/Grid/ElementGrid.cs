using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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

    
    // list with positions that changed is some form this frame(or their neighbors), and should be checked next frame
    // and should be checked by physics class or some other class next frame

    private HashSet<(int x, int y)> CheckNextFrame = new HashSet<(int x, int y)>();


    // list with positions that were already checked by physics calculations
    // and should be ignored this frame
    private HashSet<(int x, int y)> IgnoreThisFrame = new HashSet<(int x, int y)>();

    public ElementGrid(int width, int height, SandboxPixelRenderer sandboxPixelRenderer)
    {
        this.height = height;
        this.width = width;
        this.sandboxPixelRenderer = sandboxPixelRenderer;

        elementGrid = new BaseElement[width, height];
    }

    // returns positionsChangedThisFrame as array and clears it
    public HashSet<(int x, int y)> CollectCheckNextFramePosition()
    {
        var buffer = new HashSet<(int x, int y)>(CheckNextFrame);

        CheckNextFrame.Clear();
        
        return buffer;
    }
    
    public bool IsIgnorePosition(int x, int y)
    {
        return IgnoreThisFrame.Contains((x, y));
    }

    public void ClearIgnoreThisFramePositionsList()
    {
        IgnoreThisFrame.Clear();
    }

    // swaps 2 elements if both are in bound. Also works if one of elements is null
    public void SwapElements(int x1, int y1, int x2, int y2)
    {
        if (IsInBounds(x1, y1) && IsInBounds(x2, y2))
        {
            BaseElement buff = elementGrid[x2, y2];
            elementGrid[x2, y2] = elementGrid[x1, y1];
            elementGrid[x1, y1] = buff;

            IgnoreThisFrame.Add((x1, y1));
            IgnoreThisFrame.Add((x2, y2));
            AddSuroundingPositionsToCheckNextFrame(x1, y1);
            AddSuroundingPositionsToCheckNextFrame(x2, y2);

            UpdateColorValues(x2, y2);
            UpdateColorValues(x1, y1);
        }
    }

    // returns BaseElement for given position if in bounds, otherwise returns null
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


    // sets element and updates pixel color for it,
    // if position is in bounds and no other BaseElement is assigned for it
    public void SetElementIfEmpty(int x, int y, BaseElement element)
    {
        if (!ElementPresent(x, y))
        {
            elementGrid[x, y] = element;

            IgnoreThisFrame.Add((x, y));
            AddSuroundingPositionsToCheckNextFrame(x, y);

            UpdateColorValues(x, y);
        }
    }


    // sets element and updates pixel color for it,
    // if position is in bounds
    public void SetElement(int x, int y, BaseElement element)
    {
        if (IsInBounds(x, y))
        {
            elementGrid[x, y] = element;

            IgnoreThisFrame.Add((x, y));
            AddSuroundingPositionsToCheckNextFrame(x, y);

            UpdateColorValues(x, y);
        }
    }

    // update sandboxPixelRenderer pixel for given position
    private void UpdateColorValues(int x, int y)
    {

        BaseElement element = GetElement(x, y);
        Color color = Color.clear;

        if (element != null) color = element.color;

        sandboxPixelRenderer.SetPixel(x, y, color);
    }

    public bool IsInBoundsAndEmpty(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height && elementGrid[x, y] == null;
    }


    // adds all surounding positions to positionsChangedThisFrame
    public void AddSuroundingPositionsToCheckNextFrame(int x, int y)
    {
        CheckNextFrame.Add((x, y));

        // bottom
        CheckNextFrame.Add((x, y - 1));

        // left
        CheckNextFrame.Add((x - 1, y));
        // up
        CheckNextFrame.Add((x, y + 1));
        // right
        CheckNextFrame.Add((x + 1, y));

        // left up
        CheckNextFrame.Add((x - 1, y + 1));
        // right down
        CheckNextFrame.Add((x + 1, y - 1));

        // left bottom
        CheckNextFrame.Add((x - 1, y - 1));

        // top right
        CheckNextFrame.Add((x + 1, y + 1));
    }

    public void AddPositionToCheckNextFrame(int x, int y)
    {
        CheckNextFrame.Add((x, y));
    }

    // TODO I should how big is performance boost if i use inline checks instead 
    public bool IsInBounds(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    public bool ElementPresent(int x, int y)
    {
        return IsInBounds(x, y) && elementGrid[x, y] != null;
    }
}
