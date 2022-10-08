using System;
using System.Collections.Generic;
using UnityEngine;

public class UserInput
{
    private readonly ElementSpawner elementSpawner;

    private readonly float gridPixelSizeInUnityUnits;

    private readonly ChoosenBrushAndElement choosenBrushAndElement;

    public UserInput(ElementSpawner elementSpawner, float gridPixelSizeInUnityUnits, ChoosenBrushAndElement choosenBrushAndElement)
    {
        this.elementSpawner = elementSpawner;
        this.gridPixelSizeInUnityUnits = gridPixelSizeInUnityUnits;
        this.choosenBrushAndElement = choosenBrushAndElement;
    }


    public void CheckUserInput()
    {
        CheckForClickOnGrid();
    }

    public void CheckForClickOnGrid()
    {
        // Work in progress.......
        // Mobile inputs should be added
        if (Input.GetMouseButton(0))
        {
            Vector2Int inputPosition = GetMousePositionInGrid();
            SpawnBlocks(inputPosition);
        }
    }

    public Vector2Int GetMousePositionInGrid()
    {
        Vector2 worldPosition = new();

        worldPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        worldPosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        return WorldPointToGrid(worldPosition);
    }

    public Vector2Int WorldPointToGrid(Vector2 worldPosition)
    {
        Vector2Int positionInGrid = new Vector2Int();

        positionInGrid.x = Mathf.RoundToInt(worldPosition.x / gridPixelSizeInUnityUnits);
        positionInGrid.y = Mathf.RoundToInt(worldPosition.y / gridPixelSizeInUnityUnits);

        return positionInGrid;
    }

    public void SpawnBlocks(Vector2Int positionInGrid)
    {
        if (choosenBrushAndElement.elementChoosen == 0)
        {
            elementSpawner.Clear(positionInGrid.x, positionInGrid.y, new QuadSolidBrush());
        }
        else
        {
            elementSpawner.SpawnElement(positionInGrid.x, positionInGrid.y, Brushes.brushes[choosenBrushAndElement.brushChoosen], Elements.elementsTypes[choosenBrushAndElement.elementChoosen], choosenBrushAndElement.replaceElementsWithBrush);
        }
    }
}
