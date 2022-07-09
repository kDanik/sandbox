public class GasPhysics
{
    private ElementGrid elementGrid;


    public GasPhysics(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;
    }

    public void Simulate(int x, int y, Gas gas)
    {
        // get alls available positions and their weights
        var weightedAvailablePostions = GetWeightedAvailablePositions(x, y, gas);

        // randomly (with weight) choose position to move to
        var randomPosition = WeightedRandom.GetRandomPosition(weightedAvailablePostions, x, y);

        // swap to choosen position
        if (x != randomPosition.x || y != randomPosition.y) elementGrid.SwapElements(x, y, randomPosition.x, randomPosition.y);
    }

    // gets all available positions for element to move and calulates their weight, depending on type of gas and which positions are free or occupied by other gas
    private uint[,] GetWeightedAvailablePositions(int x, int y, Gas gas)
    {
        // this array represents position around x, y and their weights
        //
        // [x - 1, y + 1] [x, y + 1] [x + 1, y + 1]
        // [x - 1, y] [x, y (current element)] [x + 1, y]
        // [x - 1, y - 1] [x, y - 1] [x + 1, y - 1]
        uint[,] weightedOptions = new uint[3,3];


        // get information about all surrounding elements 
        var topElementInfo = GetElementInfo(x, y + 1);
        var bottomElementInfo = GetElementInfo(x, y - 1);

        var topLeftElementInfo = GetElementInfo(x - 1, y + 1);
        var bottomRightElementInfo = GetElementInfo(x + 1, y - 1);

        var topRightElementInfo = GetElementInfo(x + 1, y + 1);
        var bottomLeftElementInfo = GetElementInfo(x - 1, y - 1);

        var rightElementInfo = GetElementInfo(x + 1, y);
        var leftElementInfo = GetElementInfo(x - 1, y);



        // top 
        if (topElementInfo.isSwappable)
        {
            // depending on how much gas lighter or heavier than air there is more chance for some positions
            // (lighter --> more chance for going up)
            if (Gas.airMolarMass / gas.molarMass > 2)
            {
                weightedOptions[1, 0] += 10;
            }
            else if (Gas.airMolarMass / gas.molarMass > 1.1f)
            {
                weightedOptions[1, 0] += 2;
            }
            else
            {
                weightedOptions[1, 0] += 1;
            }
        }
        else if (topElementInfo.isGas && bottomElementInfo.isSwappable)
        {
            // if position has element of type gas,
            // current gas will have more chance to go on oposite position
            // (top element is gas --> more chance for current element to go down)
            weightedOptions[1, 2] += 10;
        }


        // top left
        if (topLeftElementInfo.isSwappable)
        {
            if (Gas.airMolarMass / gas.molarMass > 2)
            {
                weightedOptions[0, 0] += 10;
            }
            else if (Gas.airMolarMass / gas.molarMass > 1.1f)
            {
                weightedOptions[0, 0] += 2;
            }
            else
            {
                weightedOptions[0, 0] += 1;
            }
        }
        else if (topLeftElementInfo.isGas && bottomRightElementInfo.isSwappable)
        {
            weightedOptions[2, 2] += 10;
        }


        // top right
        if (topRightElementInfo.isSwappable)
        {
            if (Gas.airMolarMass / gas.molarMass > 2)
            {
                weightedOptions[2, 0] += 10;
            }
            else if (Gas.airMolarMass / gas.molarMass > 1.1f)
            {
                weightedOptions[2, 0] += 2;
            }
            else
            {
                weightedOptions[2, 0] += 1;
            }
        }
        else if (topRightElementInfo.isGas && bottomLeftElementInfo.isSwappable)
        {
            weightedOptions[0, 2] += 10;
        }


        // right
        if (rightElementInfo.isSwappable)
        {
            weightedOptions[2, 1] += 3;
        }
        else if (rightElementInfo.isGas && leftElementInfo.isSwappable)
        {
            weightedOptions[0, 1] += 10;
        }

        // left
        if (leftElementInfo.isSwappable)
        {
            weightedOptions[0, 1] += 3;
        }
        else if (leftElementInfo.isGas && rightElementInfo.isSwappable)
        {
            weightedOptions[2, 1] += 10;
        }


        // bottom
        if (bottomElementInfo.isSwappable)
        {
            if (Gas.airMolarMass / gas.molarMass < 0.5f)
            {
                weightedOptions[1, 2] += 5;
            }
            else if (Gas.airMolarMass / gas.molarMass < 0.8f)
            {
                weightedOptions[1, 2] += 2;
            }
            else
            {
                weightedOptions[1, 2] += 1;
            }
        }
        else if (bottomElementInfo.isGas && topElementInfo.isSwappable)
        {
            weightedOptions[1, 0] += 10;
        }


        // bottom right
        if (bottomRightElementInfo.isSwappable)
        {
            if (Gas.airMolarMass / gas.molarMass < 0.5f)
            {
                weightedOptions[2, 2] += 5;
            }
            else if (Gas.airMolarMass / gas.molarMass < 0.8f)
            {
                weightedOptions[2, 2] += 2;
            }
            else
            {
                weightedOptions[2, 2] += 1;
            }
        }
        else if (bottomRightElementInfo.isGas && topLeftElementInfo.isSwappable)
        {
            weightedOptions[0, 0] += 10;
        }


        // bottom left
        if (bottomLeftElementInfo.isSwappable)
        {
            if (Gas.airMolarMass / gas.molarMass < 0.5f)
            {
                weightedOptions[0, 2] += 5;
            }
            else if (Gas.airMolarMass / gas.molarMass < 0.8f)
            {
                weightedOptions[0, 2] += 2;
            }
            else
            {
                weightedOptions[0, 2] += 1;
            }
        }
        else if (bottomLeftElementInfo.isGas && topRightElementInfo.isSwappable)
        {
            weightedOptions[2, 0] += 10;
        }
        return weightedOptions;
    }


    struct ElementInfo
    {
        public bool isSwappable;
        public bool isGas;
    }

    // get ElementInfo for given position
    private ElementInfo GetElementInfo(int x, int y)
    {
        ElementInfo elementInfo;

        // if not in bounds then position is not swappable
        if (!elementGrid.IsInBounds(x, y))
        {
            elementInfo.isSwappable = false;
            elementInfo.isGas = false;

            return elementInfo;
        }

        BaseElement elementToSwapWith = elementGrid.GetElementUnsafe(x, y);

        // if in bounds and element is null, then position is free and swapable
        if (elementToSwapWith == null)
        {
            elementInfo.isSwappable = true;
            elementInfo.isGas = false;

            return elementInfo;
        }

        if (elementToSwapWith.IsGas())
        {
            elementInfo.isSwappable = false;
            elementInfo.isGas = true;

            return elementInfo;
        }

        elementInfo.isSwappable = false;
        elementInfo.isGas = false;

        return elementInfo;
        
    }
}
