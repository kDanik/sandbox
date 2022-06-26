public class LiquidPhysics
{
    private ElementGrid elementGrid;


    public LiquidPhysics(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;
    }

    public void Simulate(int x, int y, Liquid element)
    {
        if (TrySwapWithBottomElement(x, y, element)) return;

        SimulateLiquidFlow(x, y, element);
    }

    private void SimulateLiquidFlow(int x, int y, Liquid element)
    {
        if (element.IsFlowDirectionRight())
        {
            if (TrySwapWithBottomRightElement(x, y, element))
            {
                x++;
                y--;
            }

            if (TrySwapWithRightElement(x, y, element)) return;

            element.SwitchFlowDirectionToLeft();

            if (IsSwappableElement(x - 1, y, element)) elementGrid.AddPositionToCheckNextFrame(x, y);

        }
        else
        {
            if (TrySwapWithBottomLeftElement(x, y, element))
            {
                x--;
                y--;
            }

            if (TrySwapWithLeftElement(x, y, element)) return;

            element.SwitchFlowDirectionToRight();

            if (IsSwappableElement(x + 1, y, element)) elementGrid.AddPositionToCheckNextFrame(x, y);

        }
    }

    private bool TrySwapWithBottomElement(int x, int y, Liquid element)
    {
        if (IsSwappableElement(x, y - 2, element))
        {
            elementGrid.SwapElements(x, y, x, y - 2);

            return true;
        }


        if (!IsSwappableElement(x, y - 1, element)) return false;

        elementGrid.SwapElements(x, y, x, y - 1);

        return true;
    }
    private bool TrySwapWithLeftElement(int x, int y, Liquid element)
    {
        if (IsSwappableElement(x - 2, y, element))
        {
            if (IsSwappableElement(x - 2, y - 1, element))
            {
                elementGrid.SwapElements(x, y, x - 2, y - 1);
            }
            else
            {
                elementGrid.SwapElements(x, y, x - 2, y);
            }

            return true;
        }

        if (!IsSwappableElement(x - 1, y, element)) return false;

        elementGrid.SwapElements(x, y, x - 1, y);

        return true;
    }
    private bool TrySwapWithRightElement(int x, int y, Liquid element)
    {
        if (IsSwappableElement(x + 2, y, element))
        {
            if (IsSwappableElement(x + 2, y - 1, element))
            {
                elementGrid.SwapElements(x, y, x + 2, y - 1);
            }
            else
            {
                elementGrid.SwapElements(x, y, x + 2, y);
            }

            return true;
        }

        if (!IsSwappableElement(x + 1, y, element)) return false;

        elementGrid.SwapElements(x, y, x + 1, y);

        return true;
    }
    private bool TrySwapWithBottomLeftElement(int x, int y, Liquid element)
    {
        if (!IsSwappableElement(x - 1, y - 1, element)) return false;

        elementGrid.SwapElements(x, y, x - 1, y - 1);

        TrySwapWithBottomElement(x - 1, y - 1, element);

        return true;
    }

    private bool TrySwapWithBottomRightElement(int x, int y, Liquid element)
    {
        if (!IsSwappableElement(x + 1, y - 1, element)) return false;

        elementGrid.SwapElements(x, y, x + 1, y - 1);

        TrySwapWithBottomElement(x + 1, y - 1, element);

        return true;
    }

    private bool IsSwappableElement(int x, int y, Liquid element)
    {
        if (!elementGrid.IsInBounds(x, y)) return false;

        BaseElement elementToSwapWith = elementGrid.GetElement(x, y);

        if (elementToSwapWith == null) return true;

        if (elementToSwapWith is Gas) return true;


        if (elementToSwapWith is Liquid liquidToSwapWith && liquidToSwapWith.weight < element.weight) return true;

        return false;
    }
}
