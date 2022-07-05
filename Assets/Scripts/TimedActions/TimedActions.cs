using System;
using System.Collections.Generic;
using UnityEngine;

public class TimedActions
{
    private ElementGrid elementGrid;

    private static Queue<(uint iterationsBeforeAction, BaseElement element)> timedActions = new(10000);


    public TimedActions(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;
    }


    public static void AddTimedAction(uint iterationsBeforeAction, BaseElement element)
    {
        if (iterationsBeforeAction == 0 || element == null) throw new ArgumentException();

        timedActions.Enqueue((iterationsBeforeAction, element));
    }


    // TODO this is quite slow! maybe
    // i have to write my own queue or use linkedlist??
    public void CheckTimedActions()
    {
        int currentCount = timedActions.Count;

        for (int i = 0; i < currentCount; i++)
        {
            var currentTimedAction = timedActions.Dequeue();

            if (currentTimedAction.element == null || !currentTimedAction.element.IsOnElementGrid())
            {
                continue;
            }

            currentTimedAction.iterationsBeforeAction--;


            if (currentTimedAction.iterationsBeforeAction <= 0)
            {
                currentTimedAction.element.TimedAction(elementGrid);

                continue;
            }

            timedActions.Enqueue(currentTimedAction);
        }
    }
}
