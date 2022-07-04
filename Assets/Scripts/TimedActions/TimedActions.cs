using System;
using System.Collections.Generic;
using UnityEngine;

public class TimedActions
{
    private ElementGrid elementGrid;

    private static List<(uint iterationsBeforeAction, BaseElement element)> timedActions = new(10000);

    public TimedActions(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;
    }

    public static void AddTimedAction(uint iterationsBeforeAction, BaseElement element)
    {
        if (iterationsBeforeAction == 0 || element == null) throw new ArgumentException();

        timedActions.Add((iterationsBeforeAction, element));
    }

    public void CheckTimedActions()
    {
        for (int i = 0; i < timedActions.Count; i++)
        {
            var timedAction = timedActions[i];

            if (timedAction.element == null || !timedAction.element.IsOnElementGrid())
            {
                timedActions.RemoveAt(i);

                continue;
            }

            timedAction.iterationsBeforeAction--;


            if (timedAction.iterationsBeforeAction <= 0)
            {
                timedAction.element.TimedAction(elementGrid);
                timedActions.RemoveAt(i);

                continue;
            }

            timedActions[i] = timedAction;
        }
    }
}
