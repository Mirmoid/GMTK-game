public interface ITimerSystem
{
    float TimeRemaining { get; }
    bool IsTimerExpired { get; }
    void UpdateTimer(float deltaTime);
}

public interface IRainEffectSystem
{
    bool IsRaining { get; }
    void StartRain();
    void StopRain();
    void UpdateRainDamage(float deltaTime, IHealthSystem healthSystem);
}