using UnityEngine;

public class Smoke : Gas
{
    public Smoke() : base(10.1f, RoomTemperature, CreateRandomStoneColor()) // should research more about weight of gasses, maybe molar mass is not best representation
    {
    }

    private static Color CreateRandomStoneColor()
    {
        float allColorValues = Random.Range(0.0f, 0.4f);
        float alpha = 0.7f;

        return new Color(allColorValues, allColorValues, allColorValues, alpha);
    }
}
