using System.Collections.Generic;
using UnityEngine;

public class QuadBigRandomisedBrush : AbstractBrush
{
    public override List<Vector2Int> GetPositions(int x, int y)
    {
        var positions = new List<Vector2Int>();

        for (int xTemp = x - bigBrushSize; xTemp <= x + bigBrushSize; xTemp++)
        {
            for (int yTemp = y - bigBrushSize; yTemp <= y + bigBrushSize; yTemp++)
            {
                if (Random.Range(1, 5) == 1) positions.Add(new Vector2Int(xTemp, yTemp));
            }
        }

        return positions;
    }
}
