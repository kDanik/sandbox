using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSpawner
{
    private ElementGrid elementGrid;

    public ElementSpawner(ElementGrid elementGrid) {
        this.elementGrid = elementGrid;
    }

    public void SpawnElement<ElementType>(int x, int y, IBrush brush) {
        foreach (Vector2Int position in brush.GetPositions(x, y)) {
            elementGrid.SetElement(position.x, position.y, Activator.CreateInstance<ElementType>() as BaseElement);
        }
    }


    public void Clear(int x, int y, IBrush brush)
    {
        foreach (Vector2Int position in brush.GetPositions(x, y))
        {
            elementGrid.SetElement(position.x, position.y, null);
        }
    }
}
