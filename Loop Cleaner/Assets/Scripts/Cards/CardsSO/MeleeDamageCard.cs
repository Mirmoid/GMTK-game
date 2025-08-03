using UnityEngine;

[CreateAssetMenu(fileName = "MeleeDamage", menuName = "Cards/MeleeDamage")]
public class MeleeDamageCard : CardBase
{
    [Range(1.1f, 2f)] public float damageMultiplier = 1.3f;
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
        playerStats.meleeDamageMultiplier *= damageMultiplier;
        Debug.Log($"Melee damage increased. Multiplier: {playerStats.meleeDamageMultiplier}");
    }
}