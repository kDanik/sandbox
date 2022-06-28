using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GasPhysics
{
    private ElementGrid elementGrid;


    public GasPhysics(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;
    }

    public void Simulate(int x, int y, Gas element)
    {
        List<Vector2Int> options = FindAllAvailablePositions(x, y);

        if (options.Count == 0) return;

        Vector2Int randomOption = options[Random.Range(0, options.Count)];

        elementGrid.SwapElements(x, y, randomOption.x, randomOption.y);
    }

    public List<Vector2Int> FindAllAvailablePositions(int x, int y)
    {
        List<Vector2Int> options = new();

        if (GetElementInfo(x, y + 1).isSwappable)
        {
            options.Add(new Vector2Int(x, y+1));
        }

        if (GetElementInfo(x - 1, y + 1).isSwappable)
        {
            options.Add(new Vector2Int(x-1, y +1));
        }

        if (GetElementInfo(x + 1, y + 1).isSwappable)
        {
            options.Add(new Vector2Int(x +1 , y +1 ));
        }



        if (GetElementInfo(x - 1, y).isSwappable)
        {
            options.Add(new Vector2Int(x - 1, y));
        }

        if (GetElementInfo(x +1, y).isSwappable)
        {
            options.Add(new Vector2Int(x + 1, y));
        }


        if (GetElementInfo(x - 1, y - 1).isSwappable)
        {
            options.Add(new Vector2Int(x - 1, y - 1));
        }

        if (GetElementInfo(x + 1, y - 1).isSwappable)
        {
            options.Add(new Vector2Int(x + 1, y - 1));
        }

        if (GetElementInfo(x, y - 1).isSwappable)
        {
            options.Add(new Vector2Int(x, y - 1));
        }

        return options;
    }


    struct ElementInfo
    {
        public bool isSwappable;

    }

    // get ElementInfo for given position
    private ElementInfo GetElementInfo(int x, int y)
    {
        ElementInfo elementInfo;

        // if not in bounds then position is not swappable
        if (!elementGrid.IsInBounds(x, y))
        {
            elementInfo.isSwappable = false;

            return elementInfo;
        }

        BaseElement elementToSwapWith = elementGrid.GetElement(x, y);

        // if in bounds and element is null, then position is free and swapable
        if (elementToSwapWith == null)
        {
            elementInfo.isSwappable = true;

            return elementInfo;
        } else
        {
            elementInfo.isSwappable = false;

            return elementInfo;
        }
    }
}
