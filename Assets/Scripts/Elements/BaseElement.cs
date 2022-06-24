using UnityEngine;

public class BaseElement
{
    // Unit Weight (Kg/m3)
    public uint weight;

    // Kelvin
    public uint temperature;

    public Color color;

    public const uint RoomTemperature = 293;

    public BaseElement(uint weight, uint temperature, Color color)
    {
        this.weight = weight;
        this.temperature = temperature;
        this.color = color;
    }
}
