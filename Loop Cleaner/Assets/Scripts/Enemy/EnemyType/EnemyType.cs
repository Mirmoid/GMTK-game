using UnityEngine;

[System.Serializable]
public class EnemyType
{
    public GameObject meleePrefab;
    public GameObject rangedPrefab;
    public float baseHealth = 100f;
    public float baseDamage = 10f;
    public float baseSpeed = 3f;
}