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

    /// <summary>
    /// add new TimedAction to timedActions list
    /// </summary>
    public static void AddTimedAction(uint iterationsBeforeAction, BaseElement element)
    {
        if (iterationsBeforeAction == 0 || element == null) throw new ArgumentException();

        timedActions.AddFirst(new LinkedListNode<TimedAction>(new TimedAction(element, iterationsBeforeAction)));
    }

    /// <summary>
    /// Checks TimedAction-s from timedActions list, and executes actions with iterationsBeforeAction == 0
    /// </summary>
    public void CheckTimedActions()
    {
        LinkedListNode<TimedAction> currentNode = timedActions.First;

        while (currentNode != null)
        {
            // if node value is null or element was removed from grid, delete it from list and dont check it
            if (currentNode.Value.element == null || !currentNode.Value.element.IsOnElementGrid())
            {
                var nextNode = currentNode.Next;
                timedActions.Remove(currentNode);
                currentNode = nextNode;

                continue;
            }

            currentNode.Value.iterationsBeforeAction--;

            // if currentNode iterationsBeforeAction is 0 execute action and  remove node currentNode from list
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
