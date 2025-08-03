using UnityEngine;

[CreateAssetMenu(fileName = "StaminaRegen", menuName = "Cards/StaminaRegen")]
public class StaminaRegenCard : CardBase
{
    [Range(0.2f, 1.5f)] public float regenIncrease = 0.5f;
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
        playerStats.staminaRegenRate += regenIncrease;
        Debug.Log($"Stamina regeneration increased. Current value: {playerStats.staminaRegenRate}/сек");
    }
}