using UnityEngine;

[CreateAssetMenu(fileName = "StaminaRegen", menuName = "Cards/StaminaRegen")]
public class StaminaRegenCard : CardBase
{
    [Range(0.2f, 1.5f)] public float regenIncrease = 0.5f;

    public override void ApplyEffect(PlayerStats playerStats)
    {
        playerStats.staminaRegenRate += regenIncrease;
        Debug.Log($"Stamina regeneration increased. Current value: {playerStats.staminaRegenRate}/сек");
    }
}