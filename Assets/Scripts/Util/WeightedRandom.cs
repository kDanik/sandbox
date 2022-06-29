using UnityEngine;
using System.Collections.Generic;

public class WeightedRandom
{
    public static (int x, int y) GetRandomPosition(Dictionary<(int x, int y), uint> positionWeightPairs) {

        uint weightSum = 0;

        foreach (var weight in positionWeightPairs.Values) {
            weightSum += weight;
        }

        uint randomValue = (uint)Random.Range(1, weightSum);

        foreach (var position in positionWeightPairs.Keys)
        {
            var weight = positionWeightPairs[(position)];

            if (weight == 0) continue;

            if (weight >= randomValue) return position;

            randomValue -= weight;
        }

        return (-1, -1);
    }
}
