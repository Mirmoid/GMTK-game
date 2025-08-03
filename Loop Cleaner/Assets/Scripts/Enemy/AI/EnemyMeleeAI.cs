using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyMeleeAI : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 3.5f;
    public float damage = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    public float stoppingDistance = 1.5f;
    public float rotationSpeed = 5f;

    [Header("Animations")]
    public string walkAnimationParam = "isWalking";
    public string attackAnimationParam = "attack";
    public string idleAnimationParam = "isIdle";

    private Transform _player;
    private float _lastAttackTime;
    private PlayerMovementController _playerHealth;
    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _isAttacking;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.GetComponent<PlayerMovementController>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _agent.speed = moveSpeed;
        _agent.stoppingDistance = stoppingDistance;
        _agent.angularSpeed = rotationSpeed;
    }

    private void Update()
    {
        if (_player == null || _isAttacking) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        // Управление анимациями
        _animator.SetBool(walkAnimationParam, distanceToPlayer > attackRange && _agent.velocity.magnitude > 0.1f);
        _animator.SetBool(attackAnimationParam, distanceToPlayer <= attackRange);

        if (distanceToPlayer > attackRange)
        {
            _agent.SetDestination(_player.position);
        }
        else if (Time.time > _lastAttackTime + attackCooldown)
        {
            StartCoroutine(AttackRoutine());
        }
        Vector3 direction = _player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        _agent.isStopped = true;

        // Запуск анимации атаки
        _animator.SetTrigger(attackAnimationParam);

        // Ждем перед нанесением урона (синхронизация с анимацией)
        yield return new WaitForSeconds(0.3f);

        if (Vector3.Distance(transform.position, _player.position) <= attackRange)
        {
            _playerHealth.TakeDamage(damage);
        }

        _lastAttackTime = Time.time;

        // Ждем завершения анимации
        yield return new WaitForSeconds(0.5f);

        _agent.isStopped = false;
        _isAttacking = false;
    }

    // Визуализация радиуса атаки в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}