using UnityEngine;

public class HealthSystem : IHealthSystem
{
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; }
    public bool IsAlive => CurrentHealth > 0;

    private float _healthRegenDelay;
    private float _healthRegenRate;
    private float _timeSinceLastDamage;
    private IHealthUI _healthUI;

    public HealthSystem(float maxHealth, float regenDelay, float regenRate, IHealthUI healthUI = null)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        _healthRegenDelay = regenDelay;
        _healthRegenRate = regenRate;
        _healthUI = healthUI;

        UpdateUI();
    }

    public void TakeDamage(float amount)
    {
        if (!IsAlive) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        _timeSinceLastDamage = 0f;
        UpdateUI();
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
        UpdateUI();
    }

    public void Update(float deltaTime)
    {
        if (!IsAlive || CurrentHealth >= MaxHealth) return;

        _timeSinceLastDamage += deltaTime;

        if (_timeSinceLastDamage >= _healthRegenDelay)
        {
            Heal(_healthRegenRate * deltaTime);
        }
    }

    private void UpdateUI()
    {
        _healthUI?.UpdateHealthUI(CurrentHealth, MaxHealth);
    }
}