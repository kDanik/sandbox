using System;
using UnityEngine;

public class ElementSpawner
{
    private ElementGrid elementGrid;

    public ElementSpawner(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;
    }

    public void SpawnElement(int x, int y, AbstractBrush brush, Type elementType)
    {
        foreach (Vector2Int position in brush.GetPositions(x, y))
        {
            elementGrid.SetElement(position.x, position.y, Activator.CreateInstance(elementType) as BaseElement);
        }
    }


    public void Clear(int x, int y, AbstractBrush brush)
    {
        foreach (Vector2Int position in brush.GetPositions(x, y))
        {
            elementGrid.SetElement(position.x, position.y, null);
        }
    }
}
