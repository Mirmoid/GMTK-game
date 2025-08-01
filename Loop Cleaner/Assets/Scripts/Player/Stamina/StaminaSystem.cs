using UnityEngine;

public class StaminaSystem : IStaminaSystem
{
    public float CurrentStamina { get; private set; }
    public float MaxStamina { get; }
    public bool IsExhausted => CurrentStamina <= 0;
    public bool IsRegenerating { get; private set; }

    private float _staminaRegenDelay;
    private float _staminaRegenRate;
    private float _timeSinceLastStaminaUse;
    private IStaminaUI _staminaUI;

    public StaminaSystem(float maxStamina, float regenDelay, float regenRate, IStaminaUI staminaUI = null)
    {
        MaxStamina = maxStamina;
        CurrentStamina = maxStamina;
        _staminaRegenDelay = regenDelay;
        _staminaRegenRate = regenRate;
        _staminaUI = staminaUI;
        UpdateUI();
    }

    public bool CanUseStamina(float amount) => CurrentStamina >= amount;

    public bool TryUseStamina(float amount)
    {
        if (!CanUseStamina(amount)) return false;

        CurrentStamina -= amount;
        _timeSinceLastStaminaUse = 0f;
        IsRegenerating = false;
        UpdateUI();
        return true;
    }

    public void Update(float deltaTime)
    {
        if (CurrentStamina >= MaxStamina)
        {
            IsRegenerating = false;
            return;
        }

        _timeSinceLastStaminaUse += deltaTime;

        if (_timeSinceLastStaminaUse >= _staminaRegenDelay)
        {
            IsRegenerating = true;
            CurrentStamina = Mathf.Min(MaxStamina, CurrentStamina + _staminaRegenRate * deltaTime);
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        _staminaUI?.UpdateStaminaUI(CurrentStamina, MaxStamina);
    }
}