using UnityEngine;

public class TimerSystem : ITimerSystem
{
    public float TimeRemaining { get; private set; }
    public bool IsTimerExpired => TimeRemaining <= 0f;

    private readonly float _totalTime;

    public TimerSystem(float totalTimeInSeconds)
    {
        _totalTime = totalTimeInSeconds;
        TimeRemaining = totalTimeInSeconds;
    }

    public void UpdateTimer(float deltaTime)
    {
        if (IsTimerExpired) return;

        TimeRemaining -= deltaTime;
        if (TimeRemaining < 0) TimeRemaining = 0;
    }
}