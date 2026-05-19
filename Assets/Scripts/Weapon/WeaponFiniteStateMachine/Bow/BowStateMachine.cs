using UnityEngine;

public class BowStateMachine
{
    public BowState CurrentState { get; private set; }
    public void Initialize(BowState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }
    public void ChangeState(BowState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
