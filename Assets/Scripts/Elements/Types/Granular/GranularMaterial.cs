using UnityEngine;

public abstract class GranularMaterial : BaseElement
{
    public const uint granularPhysicsId = 2;

    public GranularMaterial(uint weight, uint temperature, Color32 color, uint elementId) : base(weight, temperature, color, elementId, granularPhysicsId)
    {
    }
}
