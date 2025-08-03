using UnityEngine;

[CreateAssetMenu(fileName = "DamageFromSpeed", menuName = "Cards/DamageFromSpeed")]
public class DamageFromSpeedCard : CardBase
{
    [Range(0.1f, 0.5f)] public float multiplierIncrease = 0.2f;
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
        playerStats.damageFromSpeedMultiplier += multiplierIncrease;
        Debug.Log($"Damage from speed increased. Multiplier: {playerStats.damageFromSpeedMultiplier}");
    }
}