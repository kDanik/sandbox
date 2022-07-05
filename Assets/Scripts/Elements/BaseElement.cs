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
    public Color32 color;
    
    ///<summary>
    /// Indicates if element is currently in elementGrid and not deleted. 
    /// Relevant mostly for TimedActions because they can reference elements that are removed from grid
    ///</summary>
    private bool isOnGrid = true;
    
    
    public const uint RoomTemperature = 293;

    public BaseElement(uint weight, uint temperature, Color color, uint elementTypeId)
    {
        this.weight = weight;
        this.temperature = temperature;
        this.color = color;
        this.elementTypeId = elementTypeId;
    }

    public virtual void TimedAction(ElementGrid elementGrid) {
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

    public bool IsOnElementGrid() {
        return isOnGrid;
    }
}
