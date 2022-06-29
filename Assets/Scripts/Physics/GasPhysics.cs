using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

        // if not positions available return
        if (weightedAvailablePostions.Count == 0) return;

        // randomly (with weight) choose position to move to
        var randomPosition = WeightedRandom.GetRandomPosition(weightedAvailablePostions);

        // swap to choosen position
        elementGrid.SwapElements(x, y, randomPosition.x, randomPosition.y);
    }

    // gets all available positions for element to move and calulates their weight, depending on type of gas and which positions are free or occupied by other gas
    private Dictionary<(int x, int y), uint> GetWeightedAvailablePositions(int x, int y, Gas gas)
    {
        Dictionary<(int x, int y), uint> weightedOptions = new();


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
                AddWeightToPositionDictionary(weightedOptions, (x, y + 1), 5);
            }
            else if (Gas.airMolarMass / gas.molarMass > 1.1f)
            {
                AddWeightToPositionDictionary(weightedOptions, (x, y + 1), 2);
            }
            else
            {
                AddWeightToPositionDictionary(weightedOptions, (x, y + 1), 1);
            }
        }
        else if (topElementInfo.isGas && bottomElementInfo.isSwappable)
        {
            // if position has element of type gas,
            // current gas will have more chance to go on oposite position
            // (top element is gas --> more chance for current element to go down)
            AddWeightToPositionDictionary(weightedOptions, (x, y - 1), 5);
        }


        // top left
        if (topLeftElementInfo.isSwappable)
        {
            if (Gas.airMolarMass / gas.molarMass > 2)
            {
                AddWeightToPositionDictionary(weightedOptions, (x - 1, y + 1), 5);
            }
            else if (Gas.airMolarMass / gas.molarMass > 1.1f)
            {
                AddWeightToPositionDictionary(weightedOptions, (x - 1, y + 1), 2);
            }
            else
            {
                AddWeightToPositionDictionary(weightedOptions, (x - 1, y + 1), 1);
            }
        }
        else if (topLeftElementInfo.isGas && bottomRightElementInfo.isSwappable)
        {
            AddWeightToPositionDictionary(weightedOptions, (x + 1, y - 1), 5);
        }


        // top right
        if (topRightElementInfo.isSwappable)
        {
            if (Gas.airMolarMass / gas.molarMass > 2)
            {
                AddWeightToPositionDictionary(weightedOptions, (x + 1, y + 1), 5);
            }
            else if (Gas.airMolarMass / gas.molarMass > 1.1f)
            {
                AddWeightToPositionDictionary(weightedOptions, (x + 1, y + 1), 2);
            }
            else
            {
                AddWeightToPositionDictionary(weightedOptions, (x + 1, y + 1), 1);
            }
        }
        else if (topRightElementInfo.isGas && bottomLeftElementInfo.isSwappable)
        {
            AddWeightToPositionDictionary(weightedOptions, (x - 1, y - 1), 5);
        }


        // right
        if (rightElementInfo.isSwappable)
        {
            AddWeightToPositionDictionary(weightedOptions, (x + 1, y), 3);
        }
        else if (rightElementInfo.isGas && leftElementInfo.isSwappable)
        {
            AddWeightToPositionDictionary(weightedOptions, (x - 1, y), 5);
        }

        // left
        if (leftElementInfo.isSwappable)
        {
            AddWeightToPositionDictionary(weightedOptions, (x - 1, y), 3);
        }
        else if (leftElementInfo.isGas && rightElementInfo.isSwappable)
        {
            AddWeightToPositionDictionary(weightedOptions, (x + 1, y), 5);
        }


        // bottom
        if (bottomElementInfo.isSwappable)
        {
            if (Gas.airMolarMass / gas.molarMass < 0.5f)
            {
                AddWeightToPositionDictionary(weightedOptions, (x, y - 1), 5);
            }
            else if (Gas.airMolarMass / gas.molarMass < 0.8f)
            {
                AddWeightToPositionDictionary(weightedOptions, (x, y - 1), 2);
            }
            else
            {
                AddWeightToPositionDictionary(weightedOptions, (x, y - 1), 1);
            }
        }
        else if (bottomElementInfo.isGas && topElementInfo.isSwappable)
        {
            AddWeightToPositionDictionary(weightedOptions, (x, y + 1), 5);
        }


        // bottom right
        if (bottomRightElementInfo.isSwappable)
        {
            if (Gas.airMolarMass / gas.molarMass < 0.5f)
            {
                AddWeightToPositionDictionary(weightedOptions, (x + 1, y - 1), 5);
            }
            else if (Gas.airMolarMass / gas.molarMass < 0.8f)
            {
                AddWeightToPositionDictionary(weightedOptions, (x + 1, y - 1), 2);
            }
            else
            {
                AddWeightToPositionDictionary(weightedOptions, (x + 1, y - 1), 1);
            }
        }
        else if (bottomRightElementInfo.isGas && topLeftElementInfo.isSwappable)
        {
            AddWeightToPositionDictionary(weightedOptions, (x - 1, y + 1), 5);
        }


        // bottom left
        if (bottomLeftElementInfo.isSwappable)
        {
            if (Gas.airMolarMass / gas.molarMass < 0.5f)
            {
                AddWeightToPositionDictionary(weightedOptions, (x - 1, y - 1), 5);
            }
            else if (Gas.airMolarMass / gas.molarMass < 0.8f)
            {
                AddWeightToPositionDictionary(weightedOptions, (x - 1, y - 1), 2);
            }
            else
            {
                AddWeightToPositionDictionary(weightedOptions, (x - 1, y - 1), 1);
            }
        }
        else if (bottomLeftElementInfo.isGas && topRightElementInfo.isSwappable)
        {
            AddWeightToPositionDictionary(weightedOptions, (x + 1, y + 1), 5);
        }


        return weightedOptions;
    }

    private void AddWeightToPositionDictionary(Dictionary<(int x, int y), uint> dictionary, (int x, int y) position, uint weight)
    {
        if (dictionary.ContainsKey(position))
        {
            dictionary[position] += weight;
        }
        else
        {
            dictionary[position] = weight;
        }
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

        BaseElement elementToSwapWith = elementGrid.GetElement(x, y);

        // if in bounds and element is null, then position is free and swapable
        if (elementToSwapWith == null)
        {
            elementInfo.isSwappable = true;
            elementInfo.isGas = false;

            return elementInfo;
        }
        else if (elementToSwapWith is Gas)
        {
            elementInfo.isSwappable = false;
            elementInfo.isGas = true;

            return elementInfo;
        }
        else
        {
            elementInfo.isSwappable = false;
            elementInfo.isGas = false;

            return elementInfo;
        }
    }
}
