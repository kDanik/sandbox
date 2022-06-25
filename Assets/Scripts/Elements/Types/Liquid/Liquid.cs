using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : BaseElement
{
    private int flowDirection = Random.Range(0, 1);

    public Liquid(uint weight, uint temperature, Color color) : base(weight, temperature, color)
    {
    }

    public void SwitchFlowDirection() {
        // switches liquid flow direction to oposite one
        flowDirection = 1 - flowDirection;
    }

    public void SwitchFlowDirectionToRight()
    {
        // switches liquid flow direction to Right
        flowDirection = 1;
    }

    public void SwitchFlowDirectionToLeft()
    {
        // switches liquid flow direction to left
        flowDirection = 0;
    }

    public bool IsFlowDirectionRight() {
        return flowDirection == 1;
    }
}
