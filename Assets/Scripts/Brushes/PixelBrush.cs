using System.Collections.Generic;
using UnityEngine;

public class PixelBrush : AbstractBrush
{
    public override List<Vector2Int> GetPositions(int x, int y)
    {
        return new List<Vector2Int>() {new Vector2Int(x,y)};
    }
}
