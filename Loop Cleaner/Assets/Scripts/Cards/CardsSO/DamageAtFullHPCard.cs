using UnityEngine;

[CreateAssetMenu(fileName = "DamageAtFullHP", menuName = "Cards/DamageAtFullHP")]
public class DamageAtFullHPCard : CardBase
{
    [Range(1.1f, 2f)] public float damageMultiplier = 1.2f;

    public override void ApplyEffect(PlayerStats playerStats)
    {
        playerStats.damageAtFullHPMultiplier *= damageMultiplier;
        Debug.Log($"Damage at full HP is increased. Current multiplier: {playerStats.damageAtFullHPMultiplier}");
    }
}