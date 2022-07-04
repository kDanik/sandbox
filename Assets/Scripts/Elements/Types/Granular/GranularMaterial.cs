using UnityEngine;

public abstract class GranularMaterial : BaseElement
{
    public GranularMaterial(uint weight, uint temperature, Color32 color, uint elementId) : base(weight, temperature, color, elementId)
    {
    }
}
