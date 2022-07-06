using System;
using System.Collections.Generic;

public static class Elements
{
    public static readonly List<Type> elementsTypes = new()
    {
        null, // reserved for empty space

        typeof(Water),
        typeof(Sand),
        typeof(Methane),
        typeof(Propane),
        typeof(Smoke),
        typeof(Oil),
        typeof(Stone),
        typeof(Fire),
        typeof(BurningOil),
        typeof(Wood),
        typeof(BurningWood),
        typeof(Ash)
    };

    // element ids (idk how this can be done better :(( 
    public const uint waterId = 1;
    public const uint sandId = 2;
    public const uint methaneId = 3;
    public const uint propaneId = 4;
    public const uint smokeId = 5;
    public const uint oilId = 6;
    public const uint stoneId = 7;
    public const uint fireId = 8;
    public const uint burningOilId = 9;
    public const uint woodId = 10;
    public const uint burningWoodId = 11;
    public const uint ashId = 11;
}
