using UnityEngine;

public sealed class ElementPhysicsDispatcher
{

    private readonly ElementGrid elementGrid;

    private readonly AbstractElementPhysics granularPhysics;

    private readonly AbstractElementPhysics liquidPhysics;

    private readonly AbstractElementPhysics gasPhysics;

    public ElementPhysicsDispatcher(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;

        granularPhysics = new GranularPhysics(elementGrid);
        liquidPhysics = new LiquidPhysics(elementGrid);
        gasPhysics = new GasPhysics(elementGrid);
    }

    /// <summary>
    /// Simulates physics for element depending on its type.
    /// </summary>
    /// <param name="element">element for which physics should be simulated</param>
    public void SimulateElementPhysics(BaseElement element)
    { 
        if (element == null || element.IsSolid()  || elementGrid.IsIgnorePosition(element.x, element.y)) return;
        

        if (element.IsGranular())
        {
            granularPhysics.Simulate(element);

            return;
        }

        if (element.IsLiquid())
        {
            liquidPhysics.Simulate(element);

            return;
        }

        if (element.IsGas())
        {
            gasPhysics.Simulate(element);

            return;
        }
    }

}

