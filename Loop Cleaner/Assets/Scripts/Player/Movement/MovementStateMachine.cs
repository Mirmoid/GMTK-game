using System;
using System.Collections.Generic;

public class MovementStateMachine
{
    private IMovementState _currentState;
    private readonly Dictionary<Type, IMovementState> _states = new();

    public void AddState(IMovementState state)
    {
        _states[state.GetType()] = state;
    }

    public void ChangeState<T>() where T : IMovementState
    {
        var newState = _states[typeof(T)];

        if (!_currentState?.CanTransitionTo(newState) ?? false)
            return;

        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }
}