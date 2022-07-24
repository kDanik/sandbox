public sealed class LiquidPhysics : AbstractElementPhysics
{
    public LiquidPhysics(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;
    }

    public override void Simulate(BaseElement element)
    {
        Liquid liquid = (Liquid)element;

        int x = element.x;
        int y = element.y;


        if (TrySwapWithBottomElement(ref x, ref y, liquid) && TrySwapWithBottomElement(ref x, ref y, liquid)) return;

        SimulateLiquidFlow(x, y, liquid);
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

            if (GetElementSwapInfo(x - 1, y, element).isSwappable) elementGrid.AddPositionToCheckNextIteration(x, y);

        }
        else
        {
            TrySwapWithLeftElement(ref x, ref y, element);
            TrySwapWithBottomElement(ref x, ref y, element);
            TrySwapWithLeftElement(ref x, ref y, element);


            if (TrySwapWithLeftElement(ref x, ref y, element)) return;

            element.SwitchFlowDirectionToRight();

            if (GetElementSwapInfo(x + 1, y, element).isSwappable) elementGrid.AddPositionToCheckNextIteration(x, y);

        }
    }

    private bool TrySwapWithBottomElement(ref int x, ref int y, Liquid element)
    {
        if (GetElementSwapInfo(x, y - 2, element).isSwappable)
        {
            elementGrid.SwapElements(x, y, x, y - 2);
            y -= 2;

            return true;
        }


        if (!GetElementSwapInfo(x, y - 1, element).isSwappable) return false;

        elementGrid.SwapElements(x, y, x, y - 1);
        y -= 1;

        return true;
    }

    private bool TrySwapWithLeftElement(ref int x, ref int y, Liquid element)
    {

        if (!GetElementSwapInfo(x - 1, y, element).isSwappable)
        {
            return false;
        }

        if (!GetElementSwapInfo(x - 2, y, element).isSwappable)
        {
            if (GetElementSwapInfo(x - 1, y - 1, element).isSwappable)
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


        if (GetElementSwapInfo(x - 2, y - 1, element).isSwappable)
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
        if (!GetElementSwapInfo(x + 1, y, element).isSwappable) return false;

        if (!GetElementSwapInfo(x + 2, y, element).isSwappable)
        {
            if (GetElementSwapInfo(x + 1, y - 1, element).isSwappable)
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


        if (GetElementSwapInfo(x + 2, y - 1, element).isSwappable)
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
}
