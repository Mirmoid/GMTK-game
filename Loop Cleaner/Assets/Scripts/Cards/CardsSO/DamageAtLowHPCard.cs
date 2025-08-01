using UnityEngine;

[CreateAssetMenu(fileName = "DamageAtLowHP", menuName = "Cards/DamageAtLowHP")]
public class DamageAtLowHPCard : CardBase
{
    [Range(1.1f, 3f)] public float damageMultiplier = 1.5f;
    [Range(0.1f, 0.4f)] public float threshold = 0.3f; // 30% HP

    public override void ApplyEffect(PlayerStats playerStats)
    {
        playerStats.damageAtLowHPMultiplier *= damageMultiplier;
        Debug.Log($"Damage at low HP (<{threshold * 100}%) увеличен. Множитель: {playerStats.damageAtLowHPMultiplier}");
    }
}