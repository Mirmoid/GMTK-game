using UnityEngine;

[CreateAssetMenu(fileName = "HPRegen", menuName = "Cards/HPRegen")]
public class HPRegenCard : CardBase
{
    [Range(0.1f, 1f)] public float regenIncrease = 0.3f;
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
        playerStats.hpRegenRate += regenIncrease;
        Debug.Log($"HP regeneration increased. Current value: {playerStats.hpRegenRate}/сек");
    }
}