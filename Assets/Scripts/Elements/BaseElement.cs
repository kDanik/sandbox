using UnityEngine;

public class BaseElement
{
    // TODO maybe element id/name and it's type should be added here to improve performance by far by reducing ammount of casting needed
    // TODO maybe it should also have position stored here to simplify a lot of code

    // Unit Weight (Kg/m3)
    public uint weight;

    // Kelvin
    public uint temperature;

    public Color32 color;

    public const uint RoomTemperature = 293;

    public BaseElement(uint weight, uint temperature, Color color)
    {
        this.weight = weight;
        this.temperature = temperature;
        this.color = color;
    }
}
