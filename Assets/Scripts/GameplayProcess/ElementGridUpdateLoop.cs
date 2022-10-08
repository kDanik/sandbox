using UnityEngine;
public class ElementGridUpdateLoop
{
    private readonly ElementGrid elementGrid;

    private readonly TimedActions timedActions;

    private readonly SandboxPixelRenderer sandboxPixelRenderer;

    private readonly ElementReactions elementReactions;

    private readonly ElementPhysicsDispatcher elementPhysicsDispatcher;

    private readonly UserInput userInput;

    public ElementGridUpdateLoop(ElementGrid elementGrid, TimedActions timedActions, SandboxPixelRenderer sandboxPixelRenderer, ElementPhysicsDispatcher elementPhysicsDispatcher, ElementReactions elementReactions, UserInput userInput)
    {
        this.elementGrid = elementGrid;
        this.timedActions = timedActions;
        this.sandboxPixelRenderer = sandboxPixelRenderer;
        this.elementReactions = elementReactions;
        this.elementPhysicsDispatcher = elementPhysicsDispatcher;
        this.userInput = userInput;
    }


    public void TriggerGridUpdate()
    {
        elementGrid.PrepareCheckThisIteration();

        timedActions.CheckTimedActions();


        SimulateElementsReactionsAndPhysics();


        userInput.CheckUserInput();

        elementGrid.ClearIgnoreThisIterationPositionsList();
        sandboxPixelRenderer.ApplyCurrentChangesToTexture();
    }


    private void SimulateElementsReactionsAndPhysics()
    {
        int randomCheckDirection = Random.Range(0, 2);

        if (randomCheckDirection == 1)
        {
            for (int y = 0; y < elementGrid.GetHeight(); y++)
            {
                for (int x = 0; x < elementGrid.GetWidth(); x++)
                {
                    SimulateElementReactionsAndPhysics(x, y);
                }
            }
        }
        else
        {
            for (int y = 0; y < elementGrid.GetHeight(); y++)
            {
                for (int x = elementGrid.GetWidth() - 1; x >= 0; x--)
                {
                    SimulateElementReactionsAndPhysics(x, y);
                }
            }
        }
    }

    private void SimulateElementReactionsAndPhysics(int x, int y)
    {
        if (!elementGrid.checkThisIteration[x, y]) return;

        BaseElement element = elementGrid.GetElementUnsafe(x, y);

        if (element == null) return;

        elementReactions.CheckReactions(x, y, element);

        elementPhysicsDispatcher.SimulateElementPhysics(element);
    }
}
