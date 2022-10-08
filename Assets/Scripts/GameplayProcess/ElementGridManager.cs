using System;
using UnityEngine;
public class ElementGridManager : MonoBehaviour
{

    private SandboxPixelRenderer sandboxPixelRenderer;

    private ElementPhysicsDispatcher elementPhysicsDispatcher;

    private ElementReactions elementReactions;

    private ElementGrid elementGrid;

    private ElementSpawner elementSpawner;

    private TimedActions timedActions;

    private UserInput userInput;

    private ElementGridUpdateLoop elementGridUpdateLoop;

    private ChoosenBrushAndElement choosenBrushAndElement;

    [SerializeField]
    private float pixelSizeInUnityUnits;
    [SerializeField]
    private int width = 900;
    [SerializeField]
    private int height = 600;

    public void Start()
    {
        Setup();
    }

    private void Setup()
    {
        sandboxPixelRenderer = this.gameObject.GetComponent<SandboxPixelRenderer>();
        sandboxPixelRenderer.InitRenderObject(width, height, pixelSizeInUnityUnits);

        choosenBrushAndElement = new ChoosenBrushAndElement();

        elementGrid = new ElementGrid(width, height, sandboxPixelRenderer);

        elementPhysicsDispatcher = new ElementPhysicsDispatcher(elementGrid);

        elementReactions = new ElementReactions(elementGrid);

        elementSpawner = new ElementSpawner(elementGrid);

        timedActions = new TimedActions(elementGrid);

        userInput = new UserInput(elementSpawner, pixelSizeInUnityUnits, choosenBrushAndElement);

        elementGridUpdateLoop = new ElementGridUpdateLoop(elementGrid, timedActions, sandboxPixelRenderer, elementPhysicsDispatcher, elementReactions, userInput);

        sandboxPixelRenderer.ApplyCurrentChangesToTexture();
    }


    public void FixedUpdate()
    {
        elementGridUpdateLoop.TriggerGridUpdate();
    }


    public void ChangeElement()
    {
        if (Elements.elementsTypes.Count <= choosenBrushAndElement.elementChoosen + 1)
        {
            choosenBrushAndElement.elementChoosen = 0;
            return;
        }
        choosenBrushAndElement.elementChoosen++;
    }

    public void ChangeBrush()
    {
        if (Brushes.brushes.Count <= choosenBrushAndElement.brushChoosen + 1)
        {
            choosenBrushAndElement.brushChoosen = 0;
            return;
        }
        choosenBrushAndElement.brushChoosen++;
    }
}
