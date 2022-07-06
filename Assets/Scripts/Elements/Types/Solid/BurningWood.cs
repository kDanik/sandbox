using UnityEngine;

public class BurningWood : Solid
{
    private int health = Random.Range(5, 20);


    public BurningWood() : base(1500, RoomTemperature, CreateRandomBurningWoodColor(), Elements.burningWoodId)
    {
        TimedActions.AddTimedAction((uint)Random.Range(5, 20), this);
    }

    private static Color32 CreateRandomBurningWoodColor()
    {
        byte red = (byte)Random.Range(200, 255);
        byte green = (byte)Random.Range(55, 100);
        byte blue = (byte)Random.Range(55, 100);

        return new Color32(red, green, blue, 255);
    }

    public override void TimedAction(ElementGrid elementGrid)
    {
        health--;
        var random = Random.Range(1, 10);

        if (health <= 0) {
            if (random < 8)
            {
                elementGrid.SetElement(x, y, new Fire());
            }
            else
            {
                elementGrid.SetElement(x, y, new Ash());
            }
          
            return;
        }

        if (health > 5) ChangeElementsColor(elementGrid, ColorUtil.GetDarkerColor(GetColor(), 1.3f));


        if (health == 5) ChangeElementsColor(elementGrid, ColorUtil.GetDarkerColor(GetColor(), 2f));
  

        if (random > 5)
        {
            elementGrid.SetElementIfEmpty(x, y + 1, new Fire());
            elementGrid.SetElementIfEmpty(x + 1, y, new Fire());
            elementGrid.SetElementIfEmpty(x - 1, y, new Fire());
        }

        if (random < 2)
        {
            elementGrid.SetElementIfEmpty(x, y + 1, new Smoke());
            elementGrid.SetElementIfEmpty(x + 1, y, new Smoke());
            elementGrid.SetElementIfEmpty(x - 1, y, new Smoke());
        }

        TimedActions.AddTimedAction((uint)Random.Range(5, 20), this);
    }
}
