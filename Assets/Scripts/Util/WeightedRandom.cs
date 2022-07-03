using UnityEngine;
using System.Collections.Generic;

public class WeightedRandom
{

    // takes as input dictionary of: key - position (x, y) and unit weight
    // returns weighted random position
    public static (int x, int y) GetRandomPosition(uint[,] positionWeights, int xCenter, int yCenter)
    {

        uint weightSum = 0;

        for (int y = 0; y <= 2; y++)
        {
            for (int x = 0; x <= 2; x++)
            {
                weightSum += positionWeights[x,y];
            }
        }
           
        

        uint randomValue = (uint)Random.Range(1, weightSum);


        for (int y = 0; y <= 2; y++) {

            for (int x = 0; x <= 2; x++)
            {
                var weight = positionWeights[x, y];

                if (weight == 0) continue;

                if (weight >= randomValue) return GetPositionFromIndex(x, y, xCenter, yCenter);

                randomValue -= weight;
            }
        }
        return (xCenter, yCenter);
    }

    private static (int x, int y) GetPositionFromIndex(int x, int y, int xCenter, int yCenter)
    {
        return (xCenter - 1 + x, yCenter + 1 - y);
    }
}
