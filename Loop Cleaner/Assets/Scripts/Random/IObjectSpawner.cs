using System.Collections.Generic;
using UnityEngine;

public interface IObjectSpawner
{
    void SpawnObjects(IReadOnlyList<ISpawnPoint> spawnPoints, IReadOnlyList<GameObject> objectsToPlace, int count);
}