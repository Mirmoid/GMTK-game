using UnityEngine;

[CreateAssetMenu(fileName = "MeleeDamage", menuName = "Cards/MeleeDamage")]
public class MeleeDamageCard : CardBase
{
    [Range(1.1f, 2f)] public float damageMultiplier = 1.3f;

    public override void ApplyEffect(PlayerStats playerStats)
    {
        playerStats.meleeDamageMultiplier *= damageMultiplier;
        Debug.Log($"Melee damage increased. Multiplier: {playerStats.meleeDamageMultiplier}");
    }
}