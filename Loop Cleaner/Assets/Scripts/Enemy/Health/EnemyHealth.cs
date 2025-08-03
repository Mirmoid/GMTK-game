using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;

    public event Action OnDeath;
    public float CurrentHealth { get; set; }
    public float MaxHealth => _maxHealth;


    private void Start() => CurrentHealth = _maxHealth;

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0) Die();
        Debug.Log(damage);
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}