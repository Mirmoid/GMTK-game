using UnityEngine;

public class HealthSystem : IHealthSystem
{
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; }
    public bool IsAlive => CurrentHealth > 0;

    private float _regenDelay;
    private float _regenRate;
    private float _timeSinceLastDamage;
    private IHealthUI _healthUI;
    private PlayerDeathRespawnSystem _deathHandler;

    public HealthSystem(float maxHealth, float regenDelay, float regenRate, IHealthUI healthUI = null)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        _regenDelay = regenDelay;  // Исправлено: используем переданные параметры
        _regenRate = regenRate;    // а не дублирующие поля
        _healthUI = healthUI;
        UpdateUI();
    }

    public void SetDeathHandler(PlayerDeathRespawnSystem deathHandler)
    {
        _deathHandler = deathHandler;
    }

    public void TakeDamage(float amount)
    {
        if (!IsAlive) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        _timeSinceLastDamage = 0f;

        if (CurrentHealth <= 0)
        {
            _deathHandler?.HandleDeath();
        }

        UpdateUI();
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
        UpdateUI();
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
        _timeSinceLastDamage = 0f;
        UpdateUI();
    }

    public void Update(float deltaTime)
    {
        if (CurrentHealth >= MaxHealth) return;

        _timeSinceLastDamage += deltaTime;

        if (_timeSinceLastDamage >= _regenDelay)
        {
            CurrentHealth = Mathf.Min(CurrentHealth + _regenRate * deltaTime, MaxHealth);
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        _healthUI?.UpdateHealthUI(CurrentHealth, MaxHealth);
    }
}