using UnityEngine;

[CreateAssetMenu(fileName = "DamageAtLowHP", menuName = "Cards/DamageAtLowHP")]
public class DamageAtLowHPCard : CardBase
{
    [Range(1.1f, 3f)] public float damageMultiplier = 1.5f;
    [Range(0.1f, 0.4f)] public float threshold = 0.3f; // 30% HP
    public static PlayerStats Instance { get; private set; }
    public float damageAtFullHPMultiplier = 1f;
    public float fireRate = 1f;
    public float movementSpeed = 5f;
    public float hpRegenRate = 0.1f;
    public float staminaRegenRate = 0.2f;
    public float damageAtLowHPMultiplier = 1f;
    public float meleeDamageMultiplier = 1f;
    public float damageFromSpeedMultiplier = 1f;

    public override void ApplyEffect(PlayerStats playerStats)
    {
        playerStats.damageAtLowHPMultiplier *= damageMultiplier;
        Debug.Log($"Damage at low HP (<{threshold * 100}%) увеличен. Множитель: {playerStats.damageAtLowHPMultiplier}");
    }
}