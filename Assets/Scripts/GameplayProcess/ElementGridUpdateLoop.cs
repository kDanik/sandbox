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

        int width = elementGrid.GetWidth();
        int height = elementGrid.GetHeight();

        if (randomCheckDirection == 1)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // yes, this method could be more beautiful and splitted into separate methods
                    // but making 38000000 extra calles per fixed update and minus 200 fps will make you reconsider
                    // this has to be so ugly due to performance limitations

                    // TODO maybe this still can be optimized by only iterating some sort of list of checkThisIteration,
                    // instead of going through all positions and checking if they are in checkThisIteration.
                    //
                    // maybe checkThisIteration could be 2 dimentional list with coordinates.
                    // problem with that would be keeping this list ordered, depending on x,y , to keep same looking physics
                    // (and keeping it ordered can be big performance problem)

                    if (!elementGrid.checkThisIteration[x, y]) continue;

                    BaseElement element = elementGrid.GetElementUnsafe(x, y);

                    if (element == null) continue;

                    elementReactions.CheckReactions(x, y, element);

                    elementPhysicsDispatcher.SimulateElementPhysics(element);
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

                    elementPhysicsDispatcher.SimulateElementPhysics(element);
                }
            }
        }
    }

}
