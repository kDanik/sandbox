using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseElement
{
    public int weight;
    public Color color;
    public int temperature;

    public BaseElement(int weight, int temperature, Color color)
    {
        this.weight = weight;
        this.temperature = temperature;
        this.color = color;
    }
}
