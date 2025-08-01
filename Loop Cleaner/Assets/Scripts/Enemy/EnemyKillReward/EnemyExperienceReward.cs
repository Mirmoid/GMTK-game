using UnityEngine;

public class EnemyExperienceReward : MonoBehaviour, IExperienceReward
{
    [SerializeField] private int _expValue = 10;
    public int ExpValue => _expValue;

    private EnemyHealth _health;
    private IExperienceGainer _experienceGainer;

    private void Awake()
    {
        _health = GetComponent<EnemyHealth>();
        _experienceGainer = FindObjectOfType<PlayerExperience>();
        _health.OnDeath += OnEnemyDeath;
    }

    private void OnEnemyDeath()
    {
        _experienceGainer?.GainExperience(ExpValue);
    }

    private void OnDestroy()
    {
        _health.OnDeath -= OnEnemyDeath;
    }
}