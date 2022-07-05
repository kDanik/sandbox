using System.Collections.Generic;
using UnityEngine;

public class QuadSmallRandomisedBrush : AbstractBrush
{
    public override List<Vector2Int> GetPositions(int x, int y)
    {
        var positions = new List<Vector2Int>();

        for (int xTemp = x - smallBrushSize; xTemp <= x + smallBrushSize; xTemp++)
        {
            for (int yTemp = y - smallBrushSize; yTemp <= y + smallBrushSize; yTemp++)
            {
                if (Random.Range(1, 4) == 1) positions.Add(new Vector2Int(xTemp, yTemp));
            }
        }

        return positions;
    }
}
