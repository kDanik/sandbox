using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPhysics : MonoBehaviour
{

    private ElementGrid elementGrid;

    private GranularPhysics granularPhysics;

    public void InitElementPhysics(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;

        granularPhysics = new GranularPhysics(elementGrid);
    }

    public void SimulateElementPhysics(int x, int y, BaseElement element)
    {
        if (elementGrid.IsIgnorePosition(x, y)) return;

        if (element is GranularMaterial)
        {
            granularPhysics.Simulate(x, y);
        }
    }

}

