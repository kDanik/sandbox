using System.Collections.Generic;
using UnityEngine;

public class ElementGridController : MonoBehaviour
{
    private SandboxPixelRenderer sandboxPixelRenderer;
    private ElementPhysics elementPhysics;

    [SerializeField]
    private float pixelSizeInUnityUnits;
    [SerializeField]
    private int width = 900;
    [SerializeField]
    private int height = 600;

    [SerializeField]
    private int elementChoosenTest = 1;

    private ElementGrid elementGrid;

    private ElementSpawner elementSpawner;

    void Start()
    {
        sandboxPixelRenderer = this.gameObject.GetComponent<SandboxPixelRenderer>();
        sandboxPixelRenderer.InitRenderObject(width, height, pixelSizeInUnityUnits);

        elementGrid = new ElementGrid(width, height, sandboxPixelRenderer);

        elementPhysics = this.gameObject.GetComponent<ElementPhysics>();
        elementPhysics.InitElementPhysics(elementGrid);

        elementSpawner = new ElementSpawner(elementGrid);

        sandboxPixelRenderer.ApplyCurrentChangesToTexture();
    }

    // this part should refactored and put in another class
    void FixedUpdate()
    {
        var checkThisFrame = elementGrid.CollectCheckNextFramePosition();
        // ONLY TEST METHOD

        int randomRange = Random.Range(0, 2);

        if (randomRange == 1)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {


                    if (!checkThisFrame.Contains((x, y))) continue;

                    BaseElement element = elementGrid.GetElement(x, y);

                    if (element == null) continue;

                    elementPhysics.SimulateElementPhysics(x, y, element);
                }
            }
        }
        else
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = width; x >= 0; x--)
                {


                    if (!checkThisFrame.Contains((x, y))) continue;

                    BaseElement element = elementGrid.GetElement(x, y);

                    if (element == null) continue;

                    elementPhysics.SimulateElementPhysics(x, y, element);
                }
            }
        }



        if (Input.GetMouseButton(0))
        {
            SpawnBlocks();
        }

        elementGrid.ClearIgnoreThisFramePositionsList();
        sandboxPixelRenderer.ApplyCurrentChangesToTexture();
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
        // ONLY TEST METHOD
        List<Vector2Int> inputPositions = new List<Vector2Int>();

        inputPositions = GetWindowsInput();
        foreach (Vector2Int posInGrid in inputPositions)
        {
            if (elementChoosenTest == 0)
            {
                elementSpawner.Clear(posInGrid.x, posInGrid.y, new QuadSolidBrush());
            }
            if (elementChoosenTest == 1)
            {
                elementSpawner.SpawnElement<Water>(posInGrid.x, posInGrid.y, new QuadSolidBrush());
            }
            if (elementChoosenTest == 2)
            {
                elementSpawner.SpawnElement<Sand>(posInGrid.x, posInGrid.y, new QuadSolidBrush());
            }
            if (elementChoosenTest == 3)
            {
                elementSpawner.SpawnElement<Stone>(posInGrid.x, posInGrid.y, new QuadSolidBrush());
            }
            if (elementChoosenTest == 4)
            {
                elementSpawner.SpawnElement<Oil>(posInGrid.x, posInGrid.y, new QuadSolidBrush());
            }
        }
    }
}
