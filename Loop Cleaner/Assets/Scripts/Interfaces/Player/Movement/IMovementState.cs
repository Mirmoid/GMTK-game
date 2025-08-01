public interface IMovementState
{
    void Enter();
    void Update();
    void Exit();
    bool CanTransitionTo(IMovementState nextState);
}