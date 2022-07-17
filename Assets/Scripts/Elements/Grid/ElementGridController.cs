using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ElementGridController : MonoBehaviour
{
    private SandboxPixelRenderer sandboxPixelRenderer;
    private ElementPhysics elementPhysics;
    private ElementReactions elementReactions;

    [SerializeField]
    private float pixelSizeInUnityUnits;
    [SerializeField]
    private int width = 900;
    [SerializeField]
    private int height = 600;

    [SerializeField]
    private int elementChoosenTest = 1;

    [SerializeField]
    private int brushChoosenTest = 1;

    [SerializeField]
    private bool replaceElementsWithBrush = true;

    private ElementGrid elementGrid;

    private ElementSpawner elementSpawner;

    private TimedActions timedActions;

    void Start()
    {
        sandboxPixelRenderer = this.gameObject.GetComponent<SandboxPixelRenderer>();
        sandboxPixelRenderer.InitRenderObject(width, height, pixelSizeInUnityUnits);

        elementGrid = new ElementGrid(width, height, sandboxPixelRenderer);

        elementPhysics = this.gameObject.GetComponent<ElementPhysics>();
        elementPhysics.InitElementPhysics(elementGrid);

        elementReactions = new ElementReactions(elementGrid);

        elementSpawner = new ElementSpawner(elementGrid);

        timedActions = new TimedActions(elementGrid);

        sandboxPixelRenderer.ApplyCurrentChangesToTexture();
    }

    // this part should refactored and put in another class
    void FixedUpdate()
    {
        elementGrid.PrepareCheckThisIteration();
        // ONLY TEST METHOD

        int randomRange = Random.Range(0, 2);

        timedActions.CheckTimedActions();

        if (randomRange == 1)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    if (!elementGrid.checkThisIteration[x, y]) continue;

                    BaseElement element = elementGrid.GetElementUnsafe(x, y);

                    if (element == null) continue;

                    elementReactions.CheckReactions(x, y, element);

                    elementPhysics.SimulateElementPhysics(element);
                }
            }
        }
        else
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = width - 1; x >= 0; x--)
                {
                    if (!elementGrid.checkThisIteration[x, y]) continue;

                    BaseElement element = elementGrid.GetElementUnsafe(x, y);

                    if (element == null) continue;

                    elementReactions.CheckReactions(x, y, element);

                    elementPhysics.SimulateElementPhysics(element);
                }
            }
        }



        if (Input.GetMouseButton(0))
        {
            SpawnBlocks();
        }

        elementGrid.ClearIgnoreThisIterationPositionsList();
        sandboxPixelRenderer.ApplyCurrentChangesToTexture();
    }


    // test method!!!
    public void ChangeElement() {
        if (Elements.elementsTypes.Count <= elementChoosenTest + 1)
        {
            elementChoosenTest = 0;
            return;
        }
        elementChoosenTest++;
    }

    // test method!!!
    public void ChangeBrush()
    {
        if (Brushes.brushes.Count <= brushChoosenTest + 1)
        {
            brushChoosenTest = 0;
            return;
        }
        brushChoosenTest++;
    }


    public List<Vector2Int> GetWindowsInput()
    {
        // ONLY TEST METHOD
        List<Vector2Int> inputPositions = new List<Vector2Int>();
        inputPositions.Clear();
        Vector2Int posInGrid = new Vector2Int();
        Vector2 buff = new Vector2();

        buff.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        buff.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        posInGrid = WorldPointToGrid(buff);
        inputPositions.Add(posInGrid);
        return inputPositions;
    }

    public Vector2Int WorldPointToGrid(Vector2 input)
    {
        // ONLY TEST METHOD
        Vector2Int output = new Vector2Int();
        output.x = Mathf.RoundToInt(input.x / pixelSizeInUnityUnits);
        output.y = Mathf.RoundToInt(input.y / pixelSizeInUnityUnits);
        return output;
    }
    public void SpawnBlocks()
    {
        List<Vector2Int> inputPositions = GetWindowsInput();

        foreach (Vector2Int posInGrid in inputPositions)
        {
            if (elementChoosenTest == 0)
            {
                elementSpawner.Clear(posInGrid.x, posInGrid.y, new QuadSolidBrush());
            } else
            {
                elementSpawner.SpawnElement(posInGrid.x, posInGrid.y, Brushes.brushes[brushChoosenTest], Elements.elementsTypes[elementChoosenTest], replaceElementsWithBrush);
            }
        }
    }
}
