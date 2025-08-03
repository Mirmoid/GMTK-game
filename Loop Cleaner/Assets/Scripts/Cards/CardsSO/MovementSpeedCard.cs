using UnityEngine;

[CreateAssetMenu(fileName = "MovementSpeed", menuName = "Cards/MovementSpeed")]
public class MovementSpeedCard : CardBase
{
    [Range(0.5f, 2f)] public float speedIncrease = 0.7f;
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
        playerStats.movementSpeed += speedIncrease;
        Debug.Log($"The speed of movement has increased. Current value: {playerStats.movementSpeed}");
    }
}