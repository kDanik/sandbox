using System.Collections.Generic;
using UnityEngine;

public class QuadExtraSmallBrush : AbstractBrush
{
    public override List<Vector2Int> GetPositions(int x, int y)
    {
        var positions = new List<Vector2Int>();

        for (int xTemp = x - extraSmallBrushSize; xTemp <= x + extraSmallBrushSize; xTemp++)
        {
            for (int yTemp = y - extraSmallBrushSize; yTemp <= y + extraSmallBrushSize; yTemp++)
            {
                positions.Add(new Vector2Int(xTemp, yTemp));
            }
        }

        return positions;
    }
}