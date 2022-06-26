using System.Collections.Generic;
using UnityEngine;

public class QuadSolidBrush : IBrush
{
    private readonly int size = 5;

    List<Vector2Int> IBrush.GetPositions(int x, int y)
    {
        var positions = new List<Vector2Int>();

        for (int xTemp = x - size; xTemp <= x + size; xTemp++)
        {
            for (int yTemp = y - size; yTemp <= y + size; yTemp++)
            {
                positions.Add(new Vector2Int(xTemp, yTemp));
            }
        }

        return positions;
    }
}
