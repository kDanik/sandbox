using UnityEngine;

public class Gas : BaseElement
{
    public static float airMolarMass = 28.96f;

    public float molarMass;

    public Gas(float molarMass, uint temperature, Color color) : base(0, temperature, color)
    {
        this.molarMass = molarMass;
    }
}
