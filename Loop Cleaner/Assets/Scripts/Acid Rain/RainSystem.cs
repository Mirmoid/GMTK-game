using UnityEngine;

public class RainEffectSystem : IRainEffectSystem
{
    public bool IsRaining { get; private set; }

    private readonly ParticleSystem _rainParticles;
    private readonly float _damageInterval;
    private readonly float _damageAmount;
    private float _damageTimer;

    public RainEffectSystem(ParticleSystem rainParticles, float damageInterval, float damageAmount)
    {
        _rainParticles = rainParticles;
        _damageInterval = damageInterval;
        _damageAmount = damageAmount;
        IsRaining = false;
    }

    public void StartRain()
    {
        if (IsRaining) return;

        IsRaining = true;
        _rainParticles.Play();
    }

    public void StopRain()
    {
        if (!IsRaining) return;

        IsRaining = false;
        _rainParticles.Stop();
    }

    public void UpdateRainDamage(float deltaTime, IHealthSystem healthSystem)
    {
        if (!IsRaining || healthSystem == null) return;

        _damageTimer += deltaTime;
        if (_damageTimer >= _damageInterval)
        {
            _damageTimer = 0f;
            healthSystem.TakeDamage(_damageAmount);
        }
    }
}