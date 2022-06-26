using System.Collections.Generic;
using UnityEngine;

public interface IBrush
{
    List<Vector2Int> GetPositions(int x, int y);
}