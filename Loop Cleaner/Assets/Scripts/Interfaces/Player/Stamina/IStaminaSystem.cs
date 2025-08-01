public interface IStaminaSystem
{
    float CurrentStamina { get; }
    float MaxStamina { get; }
    bool CanUseStamina(float amount);
    bool TryUseStamina(float amount);
    bool IsExhausted { get; }
    void Update(float deltaTime);
}