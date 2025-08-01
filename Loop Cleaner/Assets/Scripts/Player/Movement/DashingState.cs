using UnityEngine;

public class DashingState : BaseMovementState
{
    private const float DashSpeed = 10f;
    private const float DashDuration = 0.3f;

    private float _dashTime;
    private Vector3 _dashDirection;

    public DashingState(PlayerMovementController controller, IStaminaSystem staminaSystem)
        : base(controller, staminaSystem) { }

    public override void Enter()
    {
        if (!StaminaSystem.TryUseStamina(MovementConstants.StaminaCostDash))
        {
            Controller.ChangeState<WalkingState>();
            return;
        }

        _dashTime = 0f;
        var input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (CameraController != null)
        {
            Vector3 forward = CameraController.CameraTransform.forward;
            Vector3 right = CameraController.CameraTransform.right;

            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            _dashDirection = forward * input.z + right * input.x;
            _dashDirection.Normalize();
        }
        else
        {
            _dashDirection = input.normalized;
        }
    }

    public override void Update()
    {
        _dashTime += Time.deltaTime;
        Controller.Move(_dashDirection, DashSpeed);

        if (_dashTime >= DashDuration)
        {
            Controller.ChangeState<WalkingState>();
        }
    }
}