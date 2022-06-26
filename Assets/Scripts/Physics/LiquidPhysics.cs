using System;
public class LiquidPhysics
{
    private ElementGrid elementGrid;


    public LiquidPhysics(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;
    }

    public void Simulate(int x, int y, Liquid element)
    {
        if (TrySwapWithBottomElement(x, y)) return;

        SimulateLiquidFlow(x, y, element);
    }

    private void SimulateLiquidFlow(int x, int y, Liquid element)
    {
        if (element.IsFlowDirectionRight())
        {
            if (TrySwapWithBottomRightElement(x, y))
            {
                x++;
                y--;
            }

            if (TrySwapWithRightElement(x, y)) return;

            element.SwitchFlowDirectionToLeft();

            if (IsSwappableElement(x - 1, y)) elementGrid.AddPositionToCheckNextFrame(x, y);

        }
        else
        {
            if (TrySwapWithBottomLeftElement(x, y))
            {
                x--;
                y--;
            }

            if (TrySwapWithLeftElement(x, y)) return;

            element.SwitchFlowDirectionToRight();

            if (IsSwappableElement(x + 1, y)) elementGrid.AddPositionToCheckNextFrame(x, y);

        }
    }

    private bool TrySwapWithBottomElement(int x, int y)
    {
        if (IsSwappableElement(x, y - 2))
        {
            elementGrid.SwapElements(x, y, x, y - 2);

            return true;
        }


        if (!IsSwappableElement(x, y - 1)) return false;

        elementGrid.SwapElements(x, y, x, y - 1);

        return true;
    }
    private bool TrySwapWithLeftElement(int x, int y)
    {
        if (IsSwappableElement(x - 2, y))
        {
            if (IsSwappableElement(x - 2, y - 1))
            {
                elementGrid.SwapElements(x, y, x - 2, y - 1);
            }
            else
            {
                elementGrid.SwapElements(x, y, x - 2, y);
            }

            return true;
        }

        if (!IsSwappableElement(x - 1, y)) return false;

        elementGrid.SwapElements(x, y, x - 1, y);

        return true;
    }
    private bool TrySwapWithRightElement(int x, int y)
    {
        if (IsSwappableElement(x + 2, y))
        {
            if (IsSwappableElement(x + 2, y - 1))
            {
                elementGrid.SwapElements(x, y, x + 2, y - 1);
            }
            else
            {
                elementGrid.SwapElements(x, y, x + 2, y);
            }

            return true;
        }

        if (!IsSwappableElement(x + 1, y)) return false;

        elementGrid.SwapElements(x, y, x + 1, y);

        return true;
    }
    private bool TrySwapWithBottomLeftElement(int x, int y)
    {
        if (!IsSwappableElement(x - 1, y - 1)) return false;

        elementGrid.SwapElements(x, y, x - 1, y - 1);

        TrySwapWithBottomElement(x - 1, y - 1);

        return true;
    }

    private bool TrySwapWithBottomRightElement(int x, int y)
    {
        if (!IsSwappableElement(x + 1, y - 1)) return false;

        elementGrid.SwapElements(x, y, x + 1, y - 1);

        TrySwapWithBottomElement(x + 1, y - 1);

        return true;
    }

    private bool IsSwappableElement(int x, int y)
    {
        return elementGrid.IsInBounds(x, y) && !elementGrid.ElementPresent(x, y);
    }
}
