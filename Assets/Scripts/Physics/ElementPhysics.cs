using UnityEngine;

public class ElementPhysics : MonoBehaviour
{

    private ElementGrid elementGrid;

    private GranularPhysics granularPhysics;

    private LiquidPhysics liquidPhysics;

    private GasPhysics gasPhysics;


    /// <summary>
    /// Initialises ElementPhysics. Maybe this can be replaced with constructor if we remove MonoBehaviour
    /// </summary>
    public void InitElementPhysics(ElementGrid elementGrid)
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

        if (element is Liquid liquidElement)
        {
            liquidPhysics.Simulate(liquidElement);
            return;
        }

        if (element is Gas gas)
        {
            gasPhysics.Simulate(gas);

            return;
        }
    }

}

