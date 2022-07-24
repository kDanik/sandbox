using System;
using UnityEngine;

public abstract class AbstractElementPhysics
{
    protected ElementGrid elementGrid;

    public abstract void Simulate(BaseElement element);


    public ElementSwapInfo GetElementSwapInfo(int x, int y, BaseElement otherElement)
    {
        ElementSwapInfo elementInfo;

        elementInfo.isSwappable = false;
        elementInfo.isLiquid = false;
        elementInfo.isGas = false;

        // if not in bounds then position is not swappable
        if (!elementGrid.IsInBounds(x, y)) return elementInfo;


        // we can use unsafe get because we check for bounds before
        BaseElement elementToSwapWith = elementGrid.GetElementUnsafe(x, y);


        // if in bounds and element is null, then position is free and swapable
        if (elementToSwapWith == null)
        {
            elementInfo.isSwappable = true;

            return elementInfo;
        }


        // get specific to element type ElementSwapInfo
        if (otherElement.IsGas()) return GetElementSwapInfoForGas(elementToSwapWith, otherElement);

        if (otherElement.IsGranular()) return GetElementSwapInfoForGranular(elementToSwapWith, otherElement);

        if (otherElement.IsLiquid()) return GetElementSwapInfoForLiquid(elementToSwapWith, otherElement);


        return elementInfo;
    }

    private ElementSwapInfo GetElementSwapInfoForGranular(BaseElement elementToSwapWith, BaseElement otherGranularElement) {
        ElementSwapInfo elementInfo;
        elementInfo.isSwappable = false;
        elementInfo.isLiquid = false;
        elementInfo.isGas = false;

        if (elementToSwapWith.IsLiquid())
        {
            elementInfo.isSwappable = true;
            elementInfo.isLiquid = true;
        }
        else if (elementToSwapWith.IsGas())
        {
            elementInfo.isSwappable = true;
            elementInfo.isGas = true;
        }

        return elementInfo;
    }

    private ElementSwapInfo GetElementSwapInfoForGas(BaseElement elementToSwapWith, BaseElement otherGasElement)
    {
        ElementSwapInfo elementInfo;
        elementInfo.isSwappable = false;
        elementInfo.isLiquid = false;
        elementInfo.isGas = false;

        if (elementToSwapWith.IsLiquid())
        {
            elementInfo.isSwappable = false;
            elementInfo.isLiquid = true;
        }
        else if (elementToSwapWith.IsGas())
        {
            elementInfo.isSwappable = false;
            elementInfo.isGas = true;
        }

        return elementInfo;
    }

    private ElementSwapInfo GetElementSwapInfoForLiquid(BaseElement elementToSwapWith, BaseElement otherLiquidElement)
    {
        ElementSwapInfo elementInfo;
        elementInfo.isSwappable = false;
        elementInfo.isLiquid = false;
        elementInfo.isGas = false;

        if (elementToSwapWith is Liquid liquidToSwapWith)
        {
            if (liquidToSwapWith.weight < otherLiquidElement.weight) elementInfo.isSwappable = true;
            elementInfo.isLiquid = true;
        }
        else if (elementToSwapWith.IsGas())
        {
            elementInfo.isSwappable = true;
            elementInfo.isGas = true;
        }

        return elementInfo;
    }

    public struct ElementSwapInfo
    {
        public bool isSwappable;
        public bool isGas;
        public bool isLiquid;
    }
}
