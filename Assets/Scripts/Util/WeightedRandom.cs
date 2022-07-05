using UnityEngine;

public static class WeightedRandom
{

    /// <summary>
    /// Returns random position around xCenter, yCenter generated with weighted random from positionWeights.
    /// </summary>
    public static (int x, int y) GetRandomPosition(uint[,] positionWeights, int xCenter, int yCenter)
    {

        uint weightSum = 0;

        // calculate weightSum from positionWeights
        for (int y = 0; y <= 2; y++)
        {
            for (int x = 0; x <= 2; x++)
            {
                weightSum += positionWeights[x, y];
            }
        }


        uint randomValue = (uint)Random.Range(1, weightSum);


        // Iterate through positionWeights and extract weight from randomValue until weight is bigger than randomValue.
        // Then return position connected with this weight
        for (int y = 0; y <= 2; y++)
        {

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

    // Calculates position on elementGrid from position of element in the center (xCenter,yCenter) and index of element from positionWeights(x,y)
    private static (int x, int y) GetPositionFromIndex(int x, int y, int xCenter, int yCenter)
    {
        return (xCenter - 1 + x, yCenter + 1 - y);
    }
}
