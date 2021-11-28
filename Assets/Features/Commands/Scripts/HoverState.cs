using System;

public static class HoverState
{
    public enum State
    {
        None,
        UI,
        Cube,
        CubeExcavate
    }
    private static State hoverState = State.None;
    public static Action onHoverStateChangedEvent;
    
    public static State currentHoverState => hoverState;

    public static void SetState(State newHoverState)
    {
        hoverState = newHoverState;
        onHoverStateChangedEvent?.Invoke();
    }
}
