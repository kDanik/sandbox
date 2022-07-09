using UnityEngine;

public abstract class Liquid : BaseElement
{
    public const uint liquidPhysicsId = 3;

    private int flowDirection = Random.Range(0, 1);

    public Liquid(uint weight, uint temperature, Color32 color, uint elementId) : base(weight, temperature, color, elementId, liquidPhysicsId)
    {
    }

    public void SwitchFlowDirection()
    {
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

    public bool IsFlowDirectionRight()
    {
        return flowDirection == 1;
    }
}
