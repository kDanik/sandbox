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
    
    /// <summary>
    /// Room temperature in Kelvin
    /// </summary>
    public const uint RoomTemperature = 293;

    /// <summary>
    /// temperature of adjacent required for FreezeReaction method to be executed from ElementReactions
    /// </summary>
    public uint freezeReactionTemperature = 0;

    /// <summary>
    /// temperature of adjacent required for HeatReaction method to be executed from ElementReactions
    /// </summary>
    public uint heatReactionTemperature = 1000000;




    public BaseElement(uint weight, uint temperature, Color color, uint elementTypeId, uint elementPhysicsTypeId)
    {
        this.weight = weight;
        this.temperature = temperature;
        this.color = color;
        this.elementTypeId = elementTypeId;
        this.elementPhysicsTypeId = elementPhysicsTypeId;
    }




    /// <summary>
    /// timed action used in TimedActions class. Should be overriden in subclass if needed.
    /// </summary>
    public virtual void TimedAction(ElementGrid elementGrid)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// freeze reaction for this element. Should be overriden if element has heat reaction and freezeReactionTemperature should be updated in subclass.
    /// </summary>
    public virtual void FreezeReaction(BaseElement elementWithLowerTempreture, ElementGrid elementGrid)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// reaction with heat for this element. Should be overriden if element has heat reaction and heatReactionTemperature should be updated in subclass.
    /// </summary>
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

    /// <summary>
    /// changes this elements color and updates color in pixelRenderer
    /// </summary>
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

    /// <summary>
    /// returns true if element has Solid physics id
    /// </summary>
    public bool IsSolid() {
        return elementPhysicsTypeId == Solid.solidPhysicsId;
    }

    /// <summary>
    /// returns true if element has Gas physics id
    /// </summary>
    public bool IsGas()
    {
        return elementPhysicsTypeId == Gas.gasPhysicsId;
    }

    /// <summary>
    /// returns true if element has Liquid physics id
    /// </summary>
    public bool IsLiquid()
    {
        return elementPhysicsTypeId == Liquid.liquidPhysicsId;
    }

    /// <summary>
    /// returns true if element has Particle physics id
    /// </summary>
    public bool IsParticle()
    {
        return elementPhysicsTypeId == Particle.particlePhysicsId;
    }

    /// <summary>
    /// returns true if element has Granular physics id
    /// </summary>
    public bool IsGranular()
    {
        return elementPhysicsTypeId == GranularMaterial.granularPhysicsId;
    }
}
