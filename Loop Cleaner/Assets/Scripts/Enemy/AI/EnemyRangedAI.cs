using UnityEngine;

public class EnemyRangedAI : MonoBehaviour
{
    [Header("Settings")]
    public float damage = 15f;
    public float attackCooldown = 3f;
    public float projectileSpeed = 8f;
    public float projectileArcHeight = 6f;
    public float aimPredictionTime = 0.5f; 

    [Header("References")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    private Transform _player;
    private float _lastAttackTime;
    private Vector3 _lastPlayerPosition;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _lastPlayerPosition = _player.position;
    }

    private void Update()
    {
        if (_player == null) return;
        Vector3 lookDirection = _player.position - transform.position;
        lookDirection.y = 0;
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        if (Time.time > _lastAttackTime + attackCooldown)
        {
            Vector3 playerVelocity = (_player.position - _lastPlayerPosition) / Time.deltaTime;
            Vector3 predictedPosition = _player.position;
            _lastPlayerPosition = _player.position;

                Shoot(predictedPosition);


            _lastAttackTime = Time.time;
        }
    }

    private void Shoot(Vector3 targetPosition)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        EnemyProjectile projectileScript = projectile.GetComponent<EnemyProjectile>();
        projectileScript.damage = damage;
        projectileScript.Setup(targetPosition);

        Vector3 direction = targetPosition - firePoint.position;
        float distance = direction.magnitude;
    }
}