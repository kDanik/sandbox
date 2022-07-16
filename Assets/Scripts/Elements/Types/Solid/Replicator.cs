
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Replicator : Solid
{
    public bool hasElementToReplicate = false;

    private Type elementTypeToReplicate;

    private readonly uint replicationFrequencyMin = 5;
    private readonly uint replicationFrequencyMax = 10;

    private bool replicatorColorSwitcher = true;


    public Replicator() : base(3000, RoomTemperature, CreateReplicatorColor(), Elements.replicatorId)
    {
    }

    public void SetElementTypeToReplicate(BaseElement element) {
        if (hasElementToReplicate) return;

        elementTypeToReplicate = element.GetType();
        hasElementToReplicate = true;
        TimedActions.AddTimedAction((uint)Random.Range(replicationFrequencyMin, replicationFrequencyMax), this);
    }

    private static Color32 CreateReplicatorColor()
    {
        return new Color32(186, 85, 211, 255);
    }

    private void SwitchReplicatorColor(ElementGrid elementGrid)
    {
        if (replicatorColorSwitcher)
        {
            ChangeElementsColor(elementGrid, new Color32(186, 85, 211, 255));
        }
        else
        {
            ChangeElementsColor(elementGrid, new Color32(186, 85, 90, 255));
        }

        replicatorColorSwitcher = !replicatorColorSwitcher;
    }

    public override void TimedAction(ElementGrid elementGrid)
    {
        ReplicateElements(elementGrid);

        TimedActions.AddTimedAction((uint)Random.Range(replicationFrequencyMin, replicationFrequencyMax), this);

        SwitchReplicatorColor(elementGrid);
    }

    // Replicate / create not more than one element per iteration
    private void ReplicateElements(ElementGrid elementGrid) {
        if (elementGrid.SetElementIfEmpty(x, y - 1, Activator.CreateInstance(elementTypeToReplicate) as BaseElement)) return;
        if (elementGrid.SetElementIfEmpty(x, y + 1, Activator.CreateInstance(elementTypeToReplicate) as BaseElement)) return;

        // use replicatorColorSwitcher to check/spawn right and left periodically 
        if (replicatorColorSwitcher)
        {
            if (elementGrid.SetElementIfEmpty(x - 1, y, Activator.CreateInstance(elementTypeToReplicate) as BaseElement)) return;
            if (elementGrid.SetElementIfEmpty(x + 1, y, Activator.CreateInstance(elementTypeToReplicate) as BaseElement)) return;
        }
        else
        {
            if (elementGrid.SetElementIfEmpty(x + 1, y, Activator.CreateInstance(elementTypeToReplicate) as BaseElement)) return;
            if (elementGrid.SetElementIfEmpty(x - 1, y, Activator.CreateInstance(elementTypeToReplicate) as BaseElement)) return;
        }
    }

}
