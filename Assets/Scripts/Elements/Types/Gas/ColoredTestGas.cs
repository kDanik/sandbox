using UnityEngine;

// this is gas with bright color for testing gas physics
public class ColoredTestGas : Gas
{
    public ColoredTestGas() : base(10f, RoomTemperature, Color.red)
    {
    }
}
