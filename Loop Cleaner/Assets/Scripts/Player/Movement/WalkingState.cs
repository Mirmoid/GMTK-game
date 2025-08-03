using UnityEngine;

public class WalkingState : BaseMovementState
{
    private const float WalkSpeed = 6f;

    public WalkingState(PlayerMovementController controller, IStaminaSystem staminaSystem)
        : base(controller, staminaSystem) { }

    public override void Update()
    {
        var input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Controller.MoveRelativeToCamera(input, WalkSpeed);

        if (Input.GetKeyDown(JumpKey) && Controller.IsGrounded)
        {
            Controller.ChangeState<JumpingState>();
        }

        if (Input.GetKeyDown(DashKey))
        {
            Controller.ChangeState<DashingState>();
        }

        if (Input.GetKeyDown(SlideKey))
        {
            Controller.ChangeState<SlidingState>();
        }
    }
}