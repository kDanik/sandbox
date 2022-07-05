using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBrush
{
    protected const int smallBrushSize = 4;

    protected const int mediumBrushSize = 7;

    protected const int bigBrushSize = 10;

    public abstract List<Vector2Int> GetPositions(int x, int y);
}