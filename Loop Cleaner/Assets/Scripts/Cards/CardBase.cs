using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/CardBase")]
public class CardBase : ScriptableObject
{
    public CardType cardType;
    public string cardName;
    public string description;
    public Sprite icon;

    public virtual void ApplyEffect(PlayerStats playerStats) { }
}

public enum CardType
{
    DamageAtFullHP,
    FireRate,
    MovementSpeed,
    HPRegen,
    StaminaRegen,
    DamageAtLowHP,
    MeleeDamage,
    DamageFromSpeed
}