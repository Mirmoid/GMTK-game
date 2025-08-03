using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomObjectSpawner : IObjectSpawner
{
    public void SpawnObjects(IReadOnlyList<ISpawnPoint> spawnPoints, IReadOnlyList<GameObject> objectsToPlace, int count)
    {
        if (spawnPoints == null || spawnPoints.Count < count)
            throw new System.ArgumentException("Not enough spawn points");

        if (objectsToPlace == null || objectsToPlace.Count < count)
            throw new System.ArgumentException("Not enough objects to place");

        // Выбираем случайные незанятые точки
        var availablePoints = spawnPoints.Where(p => !p.IsOccupied).ToList();
        if (availablePoints.Count < count)
            availablePoints = spawnPoints.ToList(); // Если все заняты, используем любые

        var selectedPoints = availablePoints.OrderBy(x => Random.value).Take(count).ToList();

        // Перемещаем каждый объект в свою точку
        for (int i = 0; i < count; i++)
        {
            if (i >= objectsToPlace.Count) break; // На случай, если объектов меньше чем нужно

            var point = selectedPoints[i];
            var obj = objectsToPlace[i];

            if (obj == null) continue;

            PlaceObjectAtPoint(obj, point);
            point.IsOccupied = true;
        }
    }

    private void PlaceObjectAtPoint(GameObject obj, ISpawnPoint spawnPoint)
    {
        obj.transform.SetPositionAndRotation(
            spawnPoint.Transform.position,
            spawnPoint.Transform.rotation);
        obj.SetActive(true);
    }
}