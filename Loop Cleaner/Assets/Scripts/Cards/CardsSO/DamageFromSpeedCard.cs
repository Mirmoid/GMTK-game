using UnityEngine;

[CreateAssetMenu(fileName = "DamageFromSpeed", menuName = "Cards/DamageFromSpeed")]
public class DamageFromSpeedCard : CardBase
{
    [Range(0.1f, 0.5f)] public float multiplierIncrease = 0.2f;

    public override void ApplyEffect(PlayerStats playerStats)
    {
        playerStats.damageFromSpeedMultiplier += multiplierIncrease;
        Debug.Log($"Damage from speed increased. Multiplier: {playerStats.damageFromSpeedMultiplier}");
    }
}