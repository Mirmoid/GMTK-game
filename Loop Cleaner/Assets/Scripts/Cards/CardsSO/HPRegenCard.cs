using UnityEngine;

[CreateAssetMenu(fileName = "HPRegen", menuName = "Cards/HPRegen")]
public class HPRegenCard : CardBase
{
    [Range(0.1f, 1f)] public float regenIncrease = 0.3f;

    public override void ApplyEffect(PlayerStats playerStats)
    {
        playerStats.hpRegenRate += regenIncrease;
        Debug.Log($"HP regeneration increased. Current value: {playerStats.hpRegenRate}/сек");
    }
}