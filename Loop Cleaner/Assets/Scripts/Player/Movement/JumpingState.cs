using UnityEngine;

public class JumpingState : BaseMovementState
{
    private const float JumpSpeed = 10.0f;
    private const float Gravity = 20.0f;
    private const float AirControl = 0.5f;
    private const float GroundedCheckDelay = 0.1f;

    private Vector3 _moveDirection;
    private CharacterController _characterController;
    private float _lastJumpTime;
    private bool _initialGroundedCheck;

    public JumpingState(PlayerMovementController controller, IStaminaSystem staminaSystem)
        : base(controller, staminaSystem)
    {
        _characterController = controller.GetComponent<CharacterController>();
    }

    public override void Enter()
    {
        _initialGroundedCheck = _characterController.isGrounded;

        if (!StaminaSystem.TryUseStamina(MovementConstants.StaminaCostJump) || !_initialGroundedCheck)
        {
            Controller.ChangeState<WalkingState>();
            return;
        }

        _moveDirection = Vector3.zero;
        _lastJumpTime = Time.time;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (CameraController != null)
        {
            _moveDirection = CameraController.CameraTransform.TransformDirection(input);
            _moveDirection.y = 0;
            _moveDirection.Normalize();
            _moveDirection *= 5f;
        }

        _moveDirection.y = JumpSpeed;
    }

    public override void Update()
    {
        _moveDirection.y -= Gravity * Time.deltaTime;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (CameraController != null && input.magnitude > 0.1f)
        {
            Vector3 desiredDirection = CameraController.CameraTransform.TransformDirection(input);
            desiredDirection.y = 0;
            desiredDirection.Normalize();

            _moveDirection.x = Mathf.Lerp(_moveDirection.x, desiredDirection.x * 5f, AirControl * Time.deltaTime);
            _moveDirection.z = Mathf.Lerp(_moveDirection.z, desiredDirection.z * 5f, AirControl * Time.deltaTime);
        }

        _characterController.Move(_moveDirection * Time.deltaTime);

        if (Time.time > _lastJumpTime + GroundedCheckDelay &&
            _characterController.isGrounded &&
            _moveDirection.y < 0)
        {
            Controller.ChangeState<WalkingState>();
        }
    }

    public override void Exit()
    {
        _moveDirection = Vector3.zero;
    }
}