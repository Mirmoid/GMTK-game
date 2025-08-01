using UnityEngine;

public class EnemyMeleeAI : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    public float damage;
    public float attackRange = 2f;
    public float attackCooldown = 1f;

    private Transform _player;
    private float _lastAttackTime;
    private PlayerMovementController _playerHealth;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.GetComponent<PlayerMovementController>();
    }

    private void Update()
    {
        if (_player == null) return;

        Vector3 direction = _player.position - transform.position;
        direction.y = 0;

        transform.rotation = Quaternion.LookRotation(direction);

        if (direction.magnitude > attackRange)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else if (Time.time > _lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _lastAttackTime = Time.time;
        _playerHealth.TakeDamage(damage);
    }
}