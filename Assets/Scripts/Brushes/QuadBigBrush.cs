using UnityEngine;
using System.Collections.Generic;

public class QuadBigBrush : AbstractBrush
{
    public override List<Vector2Int> GetPositions(int x, int y)
    {
        var positions = new List<Vector2Int>();

        for (int xTemp = x - bigBrushSize; xTemp <= x + bigBrushSize; xTemp++)
        {
            for (int yTemp = y - bigBrushSize; yTemp <= y + bigBrushSize; yTemp++)
            {
                positions.Add(new Vector2Int(xTemp, yTemp));
            }
        }

        return positions;
    }
}
