using UnityEngine;

public interface ISpawnPoint
{
    Transform Transform { get; }
    bool IsOccupied { get; set; }
}