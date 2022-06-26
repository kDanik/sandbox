using UnityEngine;

public class Stone : Solid
{
    public Stone() : base(3000, RoomTemperature, CreateRandomStoneColor())
    {
    }

    private static Color CreateRandomStoneColor()
    {
        float allColorValues = Random.Range(0.4f, 0.70f);
        float alpha = 1;

        return new Color(allColorValues, allColorValues, allColorValues, alpha);
    }
}
