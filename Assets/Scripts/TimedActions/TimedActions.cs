using System;
using System.Collections.Generic;
using UnityEngine;

public class TimedActions
{
    private readonly ElementGrid elementGrid;

    private static LinkedList<TimedAction> timedActions = new();


    public TimedActions(ElementGrid elementGrid)
    {
        this.elementGrid = elementGrid;
    }


    public static void AddTimedAction(uint iterationsBeforeAction, BaseElement element)
    {
        if (iterationsBeforeAction == 0 || element == null) throw new ArgumentException();

        timedActions.AddFirst(new LinkedListNode<TimedAction>(new TimedAction(element, iterationsBeforeAction)));
    }

    public void CheckTimedActions()
    {
        LinkedListNode<TimedAction> currentNode = timedActions.First;

        while (currentNode != null)
        {
            if (currentNode.Value.element == null || !currentNode.Value.element.IsOnElementGrid())
            {
                var nextNode = currentNode.Next;
                timedActions.Remove(currentNode);
                currentNode = nextNode;

                continue;
            }

            currentNode.Value.iterationsBeforeAction--;

            if (currentNode.Value.iterationsBeforeAction <= 0)
            {
                currentNode.Value.element.TimedAction(elementGrid);

                var nextNode = currentNode.Next;
                timedActions.Remove(currentNode);
                currentNode = nextNode;

                continue;
            }

            currentNode = currentNode.Next;
        }

    }
}
