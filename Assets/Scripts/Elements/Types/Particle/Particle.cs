using UnityEngine;

public abstract class Particle : BaseElement
{
    public const uint particlePhysicsId = 4;

    public Particle(uint weight, uint temperature, Color32 color, uint elementId) : base(weight, temperature, color, elementId, particlePhysicsId)
    {
    }
}