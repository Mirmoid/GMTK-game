using UnityEngine;

[CreateAssetMenu(fileName = "MovementSpeed", menuName = "Cards/MovementSpeed")]
public class MovementSpeedCard : CardBase
{
    [Range(0.5f, 2f)] public float speedIncrease = 0.7f;

    public override void ApplyEffect(PlayerStats playerStats)
    {
        playerStats.movementSpeed += speedIncrease;
        Debug.Log($"The speed of movement has increased. Current value: {playerStats.movementSpeed}");
    }
}