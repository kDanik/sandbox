using UnityEngine;

public abstract class Gas : BaseElement
{
    public static float airMolarMass = 28.96f;

    public const uint gasPhysicsId = 1;

    public float molarMass; // should research more about weight of gasses, maybe molar mass is not best representation

    public Gas(float molarMass, uint temperature, Color32 color, uint elementId) : base(0, temperature, color, elementId, gasPhysicsId)
    {
        this.molarMass = molarMass;
    }
}
