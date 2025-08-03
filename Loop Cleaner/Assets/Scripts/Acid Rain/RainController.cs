using UnityEngine;

public class RainController : MonoBehaviour
{
    [Header("Rain Settings")]
    [SerializeField] private ParticleSystem rainParticles;
    [SerializeField] private float timeBeforeRain = 300f; // 5 минут
    [SerializeField] private float rainDamageInterval = 1f;
    [SerializeField] private float rainDamageAmount = 10f;

    private float timer;
    private float damageTimer;
    private bool isRaining;

    public bool IsRaining => isRaining;
    public float TimeRemaining => Mathf.Max(0, timeBeforeRain - timer);

    private void Start()
    {
        timer = 0f;
        damageTimer = 0f;
        isRaining = false;
        
        if (rainParticles != null)
        {
            rainParticles.Stop();
        }
    }

    private void Update()
    {
        if (!isRaining)
        {
            timer += Time.deltaTime;
            if (timer >= timeBeforeRain)
            {
                StartRain();
            }
        }
        else
        {
            ApplyRainDamage();
        }
    }

    private void StartRain()
    {
        isRaining = true;
        if (rainParticles != null)
        {
            rainParticles.Play();
        }
    }

    private void ApplyRainDamage()
    {
        damageTimer += Time.deltaTime;
        if (damageTimer >= rainDamageInterval)
        {
            damageTimer = 0f;
            PlayerMovementController.Instance.TakeDamage(rainDamageAmount);
        }
    }
}