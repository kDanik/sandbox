using System;
using UnityEngine;

public abstract class BaseElement
{
    ///<summary>
    /// Elements position in elementGrid
    ///</summary>
    public int x;
    
    ///<summary>
    /// Elements position in elementGrid
    ///</summary>
    public int y;
    
    ///<summary>
    /// Unique elements type id (check Elements.cs). Is relevant mostly for reactions calculations
    ///</summary>
    public uint elementTypeId = 0;

    ///<summary>
    /// Unique elements physics type id (check Gas.cs,Solid.cs, Liquid.cs and etc).
    ///</summary>
    private uint elementPhysicsTypeId = 0;

    ///<summary>
    /// Unit Weight (Kg/m3). Is relevant mostly for physics calculations
    ///</summary>
    public uint weight;

    ///<summary>
    /// Tempreture in Kelvin. Currently unused!
    ///</summary>
    public uint temperature;

    ///<summary>
    /// Color of pixel representing this element. Changing this color directly will not do anything, it should be modified via ElementGrid
    ///</summary>
    private Color32 color;
    
    ///<summary>
    /// Indicates if element is currently in elementGrid and not deleted. 
    /// Relevant mostly for TimedActions because they can reference elements that are removed from grid
    ///</summary>
    private bool isOnGrid = true;
    
    
    public const uint RoomTemperature = 293;

    public uint freezeReactionTemperature = 0;

    public uint heatReactionTemperature = 1000000;

    public BaseElement(uint weight, uint temperature, Color color, uint elementTypeId, uint elementPhysicsTypeId)
    {
        this.weight = weight;
        this.temperature = temperature;
        this.color = color;
        this.elementTypeId = elementTypeId;
        this.elementPhysicsTypeId = elementPhysicsTypeId;
    }

    public virtual void TimedAction(ElementGrid elementGrid)
    {
        throw new NotImplementedException();
    }


    public virtual void FreezeReaction(BaseElement elementWithLowerTempreture, ElementGrid elementGrid)
    {
        throw new NotImplementedException();
    }


    public virtual void HeatReaction(BaseElement elementWithHigherTempreture, ElementGrid elementGrid)
    {
        throw new NotImplementedException();
    }


    ///<summary>
    /// Marks element as not on grid, so TimedActions will not execute action for deleted elements
    ///</summary>
    public void Destroy(ElementGrid elementGrid)
    {
        x = -100;
        y = -100;

        isOnGrid = false;
    }

    public void ChangeElementsColor(ElementGrid elementGrid, Color32 newColor)
    {
        color = newColor;
        elementGrid.UpdateColorValues(x, y, this);
    }

    public Color32 GetColor()
    {
        return color;
    }

    public bool IsOnElementGrid() {
        return isOnGrid;
    }

    public bool IsSolid() {
        return elementPhysicsTypeId == Solid.solidPhysicsId;
    }

    public bool IsGas()
    {
        return elementPhysicsTypeId == Gas.gasPhysicsId;
    }

    public bool IsLiquid()
    {
        return elementPhysicsTypeId == Liquid.liquidPhysicsId;
    }

    public bool IsParticle()
    {
        return elementPhysicsTypeId == Particle.particlePhysicsId;
    }

    public bool IsGranular()
    {
        return elementPhysicsTypeId == GranularMaterial.granularPhysicsId;
    }
}
