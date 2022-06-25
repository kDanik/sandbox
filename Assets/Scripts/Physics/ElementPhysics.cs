using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPhysics : MonoBehaviour
{

    private ElementGrid elementGrid;

    private GranularPhysics granularPhysics;

    private LiquidPhysics liquidPhysics;

    public void InitElementPhysics(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;

        granularPhysics = new GranularPhysics(elementGrid);
        liquidPhysics = new LiquidPhysics(elementGrid);
    }

    public void SimulateElementPhysics(int x, int y, BaseElement element)
    {
        if (elementGrid.IsIgnorePosition(x, y) || element == null || element is Solid) return;

        if (element is GranularMaterial)
        {
            granularPhysics.Simulate(x, y);
            return;
        }

        if (element is Liquid liquidElement)
        {
            liquidPhysics.Simulate(x, y, liquidElement);
            return;
        }

    }

}

