using UnityEngine;

public class PlayerDeathRespawnSystem : MonoBehaviour
{
    [Header("Death Settings")]
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private float respawnDelay = 2f;

    private PlayerMovementController _movementController;
    private Vector3 _spawnPosition;
    private Quaternion _spawnRotation;
    private bool _isDead = false;

    public event System.Action OnDeath;
    public event System.Action OnRespawn;

    private void Awake()
    {
        _movementController = GetComponent<PlayerMovementController>();
        SetSpawnPoint(transform.position, transform.rotation);

        if (deathScreen != null)
            deathScreen.SetActive(false);
    }

    public void SetSpawnPoint(Vector3 position, Quaternion rotation)
    {
        _spawnPosition = position;
        _spawnRotation = rotation;
    }

    public void HandleDeath()
    {
        if (_isDead) return;

        _isDead = true;
        _movementController.SetControlEnabled(false);

        if (deathScreen != null)
            deathScreen.SetActive(true);

        OnDeath?.Invoke();
        Invoke(nameof(Respawn), respawnDelay);
    }

    public void Respawn()
    {
        transform.SetPositionAndRotation(_spawnPosition, _spawnRotation);
        _movementController.SetControlEnabled(true);

        if (deathScreen != null)
            deathScreen.SetActive(false);

        _isDead = false;
        OnRespawn?.Invoke();
    }
}