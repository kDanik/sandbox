using UnityEngine;

public abstract class Solid : BaseElement
{
    public Solid(uint weight, uint temperature, Color32 color, uint elementId) : base(weight, temperature, color, elementId)
    {
    }
}
