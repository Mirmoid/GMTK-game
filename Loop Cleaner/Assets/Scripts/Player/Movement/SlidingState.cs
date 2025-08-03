using UnityEngine;

public class SlidingState : BaseMovementState
{
    private const float SlideDuration = 1f;
    private const float SlideSpeed = 12f;
    private const float CameraHeightAdjustment = 0.5f;

    private float _slideTime;
    private Vector3 _slideDirection;
    private Vector3 _originalCameraLocalPos;
    private bool _isSliding;

    public SlidingState(PlayerMovementController controller, IStaminaSystem staminaSystem)
        : base(controller, staminaSystem) { }

    public override void Enter()
    {
        _originalCameraLocalPos = CameraController.CameraTransform.localPosition;

        _slideTime = 0f;
        _isSliding = true;
        var input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (CameraController != null)
        {
            Vector3 newCameraPos = _originalCameraLocalPos;
            newCameraPos.y -= CameraHeightAdjustment;
            CameraController.CameraTransform.localPosition = newCameraPos;
        }

        if (CameraController != null)
        {
            Vector3 forward = CameraController.CameraTransform.forward;
            Vector3 right = CameraController.CameraTransform.right;

            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            _slideDirection = forward * input.z + right * input.x;
            _slideDirection.Normalize();
        }
        else
        {
            _slideDirection = input.normalized;
        }
    }

    public override void Update()
    {
        if (!_isSliding) return;

        _slideTime += Time.deltaTime;
        Controller.Slide(_slideDirection + Vector3.down * 0.1f, SlideSpeed);

        if (_slideTime >= SlideDuration)
        {
            ExitSlide();
        }
    }

    private void ExitSlide()
    {
        if (!_isSliding) return;
        _isSliding = false;

        if (CameraController != null)
        {
            CameraController.CameraTransform.localPosition = _originalCameraLocalPos;
        }

        Controller.ChangeState<WalkingState>();
    }

    public override void Exit()
    {
        if (_isSliding)
        {
            ExitSlide();
        }
    }
}