using UnityEngine;

public class SupportWeaponStateMachine
{
    public SupportWeaponState CurrentState { get; private set; }

    public void Initialize(SupportWeaponState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(SupportWeaponState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}