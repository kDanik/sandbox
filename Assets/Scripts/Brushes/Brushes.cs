using System.Collections.Generic;

public static class Brushes
{
    public static readonly List<AbstractBrush> brushes = new()
    {
        new QuadSmallRandomisedBrush(),
        new QuadSolidBrush(),
        new QuadBigBrush()
    };
}
