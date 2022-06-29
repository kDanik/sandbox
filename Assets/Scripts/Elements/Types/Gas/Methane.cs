﻿using UnityEngine;

public class Methane : Gas
{
    public Methane() : base(16.04f, RoomTemperature, CreateRandomStoneColor())
    {
    }

    private static Color CreateRandomStoneColor()
    {
        float red = Random.Range(0.78f, 0.94f);
        float green = Random.Range(0.1f, 0.14f);
        float blue = Random.Range(0.1f, 0.14f);
        float alpha = 0.3f;

        return new Color(red, green, blue, alpha);
    }
}
