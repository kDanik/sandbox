public class LiquidPhysics
{
    private ElementGrid elementGrid;


    public LiquidPhysics(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;
    }

    public void Simulate(Liquid element)
    {
        int x = element.x;
        int y = element.y;


        if (TrySwapWithBottomElement(ref x, ref y, element) && TrySwapWithBottomElement(ref x, ref y, element)) return;

        SimulateLiquidFlow(x, y, element);
    }

    private void SimulateLiquidFlow(int x, int y, Liquid element)
    {
        if (element.IsFlowDirectionRight())
        {

            TrySwapWithRightElement(ref x, ref y, element);
            TrySwapWithBottomElement(ref x, ref y, element);
            TrySwapWithRightElement(ref x, ref y, element);

            if (TrySwapWithRightElement(ref x, ref y, element)) return;

            element.SwitchFlowDirectionToLeft();

            if (IsSwappableElement(x - 1, y, element)) elementGrid.AddPositionToCheckNextIteration(x, y);

        }
        else
        {
            TrySwapWithLeftElement(ref x, ref y, element);
            TrySwapWithBottomElement(ref x, ref y, element);
            TrySwapWithLeftElement(ref x, ref y, element);


            if (TrySwapWithLeftElement(ref x, ref y, element)) return;

            element.SwitchFlowDirectionToRight();

            if (IsSwappableElement(x + 1, y, element)) elementGrid.AddPositionToCheckNextIteration(x, y);

        }
    }

    private bool TrySwapWithBottomElement(ref int x, ref int y, Liquid element)
    {
        if (IsSwappableElement(x, y - 2, element))
        {
            elementGrid.SwapElements(x, y, x, y - 2);
            y -= 2;

            return true;
        }


        if (!IsSwappableElement(x, y - 1, element)) return false;

        elementGrid.SwapElements(x, y, x, y - 1);
        y -= 1;

        return true;
    }
    private bool TrySwapWithLeftElement(ref int x, ref int y, Liquid element)
    {

        if (!IsSwappableElement(x - 1, y, element))
        {
            return false;
        }

        if (!IsSwappableElement(x - 2, y, element))
        {
            if (IsSwappableElement(x - 1, y - 1, element))
            {
                elementGrid.SwapElements(x, y, x - 1, y - 1);
                x--;
                y--;
            }
            else
            {
                elementGrid.SwapElements(x, y, x - 1, y);
                x--;
            }
            return true;
        }


        if (IsSwappableElement(x - 2, y - 1, element))
        {
            elementGrid.SwapElements(x, y, x - 2, y - 1);
            x -= 2;
            y--;
        }
        else
        {
            elementGrid.SwapElements(x, y, x - 2, y);
            x -= 2;
        }

        return true;
    }

    private bool TrySwapWithRightElement(ref int x,ref int y, Liquid element)
    {
        if (!IsSwappableElement(x + 1, y, element)) return false;

        if (!IsSwappableElement(x + 2, y, element))
        {
            if (IsSwappableElement(x + 1, y - 1, element))
            {
                elementGrid.SwapElements(x, y, x + 1, y - 1);
                x++;
                y--;
            }
            else
            {
                elementGrid.SwapElements(x, y, x + 1, y);
                x++;
            }
            return true;
        }


        if (IsSwappableElement(x + 2, y - 1, element))
        {
            elementGrid.SwapElements(x, y, x + 2, y - 1);
            x += 2;
            y--;
        }
        else
        {
            elementGrid.SwapElements(x, y, x + 2, y);
            x += 2;
        }

        return true;
    }

    private bool IsSwappableElement(int x, int y, Liquid element)
    {
        if (!elementGrid.IsInBounds(x, y)) return false;

        BaseElement elementToSwapWith = elementGrid.GetElementUnsafe(x, y);

        if (elementToSwapWith == null) return true;

        if (elementToSwapWith.IsGas()) return true;


        if (elementToSwapWith is Liquid liquidToSwapWith && liquidToSwapWith.weight < element.weight) return true;

        return false;
    }
}
