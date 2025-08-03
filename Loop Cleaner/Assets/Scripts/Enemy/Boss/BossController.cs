using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    [System.Serializable]
    public class BossAttack
    {
        public string animationTrigger;
        public float damage;
        public float range;
        public float cooldown;
        [Range(0, 100)] public int weight = 50;
        public float animationDuration; // Длительность анимации атаки
    }

    [Header("Movement")]
    public float chaseSpeed = 3.5f;
    public float stoppingDistance = 2f;

    [Header("Attacks")]
    public BossAttack[] attacks;
    public Transform player;

    [Header("References")]
    public NavMeshAgent agent;
    public Animator animator;

    private float[] _attackCooldowns;
    private bool _isActive;
    private int _lastAttackIndex = -1;
    private bool _isAttacking;

    private void Start()
    {
        _attackCooldowns = new float[attacks.Length];
        agent.stoppingDistance = stoppingDistance;
    }

    public void ActivateBoss()
    {
        _isActive = true;
        agent.speed = chaseSpeed;
        StartChasing();
    }

    private void Update()
    {
        if (!_isActive || _isAttacking) return;

        // Преследование игрока
        agent.SetDestination(player.position);

        // Обновляем кулдауны атак
        for (int i = 0; i < _attackCooldowns.Length; i++)
        {
            if (_attackCooldowns[i] > 0)
            {
                _attackCooldowns[i] -= Time.deltaTime;
            }
        }

        // Проверка возможности атаки
        if (Vector3.Distance(transform.position, player.position) <= stoppingDistance * 1.1f)
        {
            TryAttack();
        }
    }

    private void StartChasing()
    {
        _isAttacking = false;
        agent.isStopped = false;
        animator.SetTrigger("IsRun");
    }

    private void TryAttack()
    {
        var availableAttacks = new System.Collections.Generic.List<int>();
        int totalWeight = 0;

        for (int i = 0; i < attacks.Length; i++)
        {
            if (_attackCooldowns[i] <= 0)
            {
                availableAttacks.Add(i);
                totalWeight += attacks[i].weight;
            }
        }

        if (availableAttacks.Count > 0)
        {
            int randomPoint = Random.Range(0, totalWeight);
            int accumulatedWeight = 0;
            int selectedAttackIndex = 0;

            foreach (int attackIndex in availableAttacks)
            {
                accumulatedWeight += attacks[attackIndex].weight;
                if (randomPoint < accumulatedWeight)
                {
                    selectedAttackIndex = attackIndex;
                    break;
                }
            }

            PerformAttack(selectedAttackIndex);
        }
    }

    private void PerformAttack(int attackIndex)
    {
        _isAttacking = true;
        _lastAttackIndex = attackIndex;
        _attackCooldowns[attackIndex] = attacks[attackIndex].cooldown;

        // Останавливаем движение для атаки
        agent.isStopped = true;
        animator.SetTrigger(attacks[attackIndex].animationTrigger);

        // Возвращаемся к преследованию после анимации атаки
        Invoke("StartChasing", attacks[attackIndex].animationDuration);
    }

    public void DealAttackDamage()
    {
        if (_lastAttackIndex == -1) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attacks[_lastAttackIndex].range)
        {
            player.GetComponent<PlayerMovementController>().TakeDamage(attacks[_lastAttackIndex].damage);
        }
    }
}