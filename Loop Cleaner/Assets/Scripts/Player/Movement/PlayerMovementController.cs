using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour, IMovementController
{
    public static PlayerMovementController Instance { get; private set; }

    [Header("References")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerCameraController _cameraController;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Health Settings")]
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _healthRegenDelay = 5f;
    [SerializeField] private float _healthRegenRate = 5f;

    [Header("Stamina Settings")]
    [SerializeField] private float _maxStamina = 10f;
    [SerializeField] private float _staminaRegenDelay = 2f;
    [SerializeField] private float _staminaRegenRate = 10f;

    [Header("Health UI")]
    [SerializeField] private HealthUIController _healthUIController;

    [Header("Stamina UI")]
    [SerializeField] private StaminaUIController _staminaUIController;

    private MovementStateMachine _stateMachine;
    private IHealthSystem _healthSystem;
    private IStaminaSystem _staminaSystem;
    private float _originalHeight;
    private bool _controlsEnabled = true;
    private PlayerDeathRespawnSystem _deathRespawnSystem;

    public Vector3 Velocity => _characterController.velocity;
    public bool IsGrounded => _characterController.isGrounded;
    public ICameraController CameraController => _cameraController;
    public Vector3 GetCenter() => _characterController.center;

    public float GetHeight() => _characterController.height;
    public Rigidbody GetRigidbody() => GetComponent<Rigidbody>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _characterController = GetComponent<CharacterController>();
        _originalHeight = _characterController.height;

        if (_cameraController == null)
            _cameraController = GetComponent<PlayerCameraController>();

        // Инициализация системы смерти и респавна
        _deathRespawnSystem = GetComponent<PlayerDeathRespawnSystem>();
        if (_deathRespawnSystem == null)
            _deathRespawnSystem = gameObject.AddComponent<PlayerDeathRespawnSystem>();

        _healthSystem = new HealthSystem(
            maxHealth: _maxHealth,
            regenDelay: _healthRegenDelay,
            regenRate: _healthRegenRate,
            healthUI: _healthUIController
        );

        _staminaSystem = new StaminaSystem(
            maxStamina: _maxStamina,
            regenDelay: _staminaRegenDelay,
            regenRate: _staminaRegenRate,
            staminaUI: _staminaUIController
        );

        // Связываем системы
        ((HealthSystem)_healthSystem).SetDeathHandler(_deathRespawnSystem);


        _stateMachine = new MovementStateMachine();
        _stateMachine.AddState(new WalkingState(this, _staminaSystem));
        _stateMachine.AddState(new DashingState(this, _staminaSystem));
        _stateMachine.AddState(new JumpingState(this, _staminaSystem));
        _stateMachine.AddState(new SlidingState(this, _staminaSystem));

        _stateMachine.ChangeState<WalkingState>();
    }

    private void Update()
    {
        if (!_controlsEnabled) return;
        HandleCameraRotation();
        _stateMachine.Update();
        _healthSystem.Update(Time.deltaTime);
        _staminaSystem.Update(Time.deltaTime);
    }

    private void OnRespawn()
    {
        ((HealthSystem)_healthSystem).ResetHealth();
        ChangeState<WalkingState>();
    }

    public void SetControlEnabled(bool enabled)
    {
        _controlsEnabled = enabled;
    }

    public void TakeDamage(float damage)
    {
        _healthSystem.TakeDamage(damage);
    }
    private void HandleCameraRotation()
    {
        if (_cameraController == null) return;

        var mouseInput = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
        );

        _cameraController.RotateCamera(mouseInput);
    }

    public void ChangeState<T>() where T : IMovementState => _stateMachine.ChangeState<T>();

    public void Move(Vector3 direction, float speedModifier)
    {
        if (direction.magnitude > 0)
        {
            _characterController.Move(direction * speedModifier * Time.deltaTime);
        }

        if (!IsGrounded)
        {
            _characterController.Move(Physics.gravity * Time.deltaTime);
        }
    }

    public void MoveRelativeToCamera(Vector3 inputDirection, float speedModifier)
    {
        if (_cameraController == null)
        {
            Move(inputDirection, speedModifier);
            return;
        }

        Vector3 forward = _cameraController.CameraTransform.forward;
        Vector3 right = _cameraController.CameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * inputDirection.z + right * inputDirection.x;
        Move(moveDirection.normalized, speedModifier);
    }

    public void Jump(float power)
    {
        _characterController.Move(Vector3.up * power * Time.deltaTime);
    }

    public void Slide(Vector3 direction, float speed)
    {
        var slideVector = direction.normalized * speed;
        _characterController.Move(slideVector * Time.deltaTime);
    }

    public void SetHeight(float height)
    {
        _characterController.height = Mathf.Clamp(height, 0.1f, 10f);
        UpdateCenter();
    }

    public void SetCenter(Vector3 center)
    {
        _characterController.center = center;
    }

    private void UpdateCenter()
    {
        _characterController.center = new Vector3(0, _characterController.height * 0.5f, 0);
    }
}