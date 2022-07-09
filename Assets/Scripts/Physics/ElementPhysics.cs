using UnityEngine;

public class ElementPhysics : MonoBehaviour
{

    private ElementGrid elementGrid;

    private GranularPhysics granularPhysics;

    private LiquidPhysics liquidPhysics;

    private GasPhysics gasPhysics;

    public void InitElementPhysics(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;

        granularPhysics = new GranularPhysics(elementGrid);
        liquidPhysics = new LiquidPhysics(elementGrid);
        gasPhysics = new GasPhysics(elementGrid);
    }

    public void SimulateElementPhysics(int x, int y, BaseElement element)
    {
        if (elementGrid.IsIgnorePosition(x, y) || element == null || element.IsSolid()) return;

        if (element.IsGranular())
        {
            granularPhysics.Simulate(x, y);
            return;
        }

        if (element is Liquid liquidElement)
        {
            liquidPhysics.Simulate(x, y, liquidElement);
            return;
        }

        if (element is Gas gas)
        {
            gasPhysics.Simulate(x, y, gas);

            return;
        }
    }

}

