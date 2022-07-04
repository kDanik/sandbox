using System;
using UnityEngine;

public abstract class BaseElement
{
    // TODO maybe element id/name and it's type should be added here to improve performance by far by reducing ammount of casting needed
    // TODO maybe it should also have position stored here to simplify a lot of code

    public int x;
    public int y;

    private bool isOnGrid = true;

    public uint elementTypeId = 0;

    // Unit Weight (Kg/m3)
    public uint weight;

    // Kelvin
    public uint temperature;

    public Color32 color;

    public const uint RoomTemperature = 293;

    public BaseElement(uint weight, uint temperature, Color color, uint elementTypeId)
    {
        this.weight = weight;
        this.temperature = temperature;
        this.color = color;
        this.elementTypeId = elementTypeId;
    }

    public virtual void TimedAction(ElementGrid elementGrid) {
        throw new NotImplementedException();
    }

    public void Destroy(ElementGrid elementGrid)
    {
        x = -100;
        y = -100;

        isOnGrid = false;
    }

    public bool IsOnElementGrid() {
        return isOnGrid;
    }
}
