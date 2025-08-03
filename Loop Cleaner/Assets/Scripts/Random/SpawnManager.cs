using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
    [SerializeField] private List<GameObject> _objectsToPlace = new List<GameObject>();
    [SerializeField] private int _objectsToSpawn = 2;

    private IObjectSpawner _spawner;

    private void Awake()
    {
        _spawner = new RandomObjectSpawner();

        // Деактивируем все объекты перед началом
        foreach (var obj in _objectsToPlace)
        {
            if (obj != null) obj.SetActive(false);
        }
    }

    private void Start()
    {
        SpawnObjects();
    }

    public void SpawnObjects()
    {
        _spawner.SpawnObjects(_spawnPoints, _objectsToPlace, _objectsToSpawn);
    }

    // Для отладки в редакторе
    private void OnValidate()
    {
        // Автоматически собираем все точки спавна, если список пуст
        if (_spawnPoints == null || _spawnPoints.Count == 0)
        {
            _spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();
        }
    }
}