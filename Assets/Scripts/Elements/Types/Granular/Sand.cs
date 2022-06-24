using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sand : GranularMaterial
{
    public Sand() : base(1540, RoomTemperature, CreateRandomSandColor())
    {
    }

    private static Color CreateRandomSandColor() {
        float red = Random.Range(0.78f, 0.94f);
        float green = Random.Range(0.78f, 0.94f);
        float blue = Random.Range(0.45f, 0.60f);
        float alpha = 1;

        return new Color(red, green, blue, alpha);
    }
}
