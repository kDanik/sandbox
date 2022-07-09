public class TimedAction
{
    public uint iterationsBeforeAction;
    public readonly BaseElement element;

    public TimedAction(BaseElement element, uint iterationsBeforeAction)
    {
        this.iterationsBeforeAction = iterationsBeforeAction;
        this.element = element;
    }
}
