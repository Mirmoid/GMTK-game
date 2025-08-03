using UnityEngine;

public class SpawnPoint : MonoBehaviour, ISpawnPoint
{
    public Transform Transform => transform;
    public bool IsOccupied { get; set; }
}