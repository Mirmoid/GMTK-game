public interface IHealthSystem
{
    float CurrentHealth { get; }
    float MaxHealth { get; }
    void TakeDamage(float amount);
    void Heal(float amount);
    bool IsAlive { get; }
    void Update(float deltaTime);
}