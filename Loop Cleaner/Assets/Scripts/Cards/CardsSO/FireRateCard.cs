using UnityEngine;

[CreateAssetMenu(fileName = "FireRate", menuName = "Cards/FireRate")]
public class FireRateCard : CardBase
{
    [Range(0.1f, 0.5f)] public float fireRateIncrease = 0.15f;

    public override void ApplyEffect(PlayerStats playerStats)
    {
        playerStats.fireRate += fireRateIncrease;
        Debug.Log($"The firing rate has been increased. Current value: {playerStats.fireRate}");
    }
}