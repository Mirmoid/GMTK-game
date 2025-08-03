using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private EnemyType _enemySettings;
    [SerializeField] private Transform _player;
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private int _baseEnemiesPerWave = 5;
    [SerializeField] private LayerMask _obstacleLayers; // Слои препятствий
    [SerializeField] private LayerMask _spawnSurfaceLayer; // Слой, на котором можно спавнить врагов

    [Header("Progress")]
    [SerializeField] private float _enemyCountMultiplier = 1.2f;
    [SerializeField] private float _healthMultiplier = 1.15f;
    [SerializeField] private float _damageMultiplier = 1.1f;
    [SerializeField] private float _speedMultiplier = 1.05f;

    private int _currentWave = 0;
    private int _enemiesRemaining;
    private bool _isWaveActive;
    private Coroutine _waveCycleCoroutine;

    private void Start()
    {
        _waveCycleCoroutine = StartCoroutine(WaveCycle());
    }

    private IEnumerator WaveCycle()
    {
        while (true)
        {
            if (_currentWave > 0)
            {
                Debug.Log($"Волна {_currentWave} завершена! Подготовка к следующей...");
                yield return new WaitForSeconds(_timeBetweenWaves);
            }

            StartNewWave();
            yield return new WaitForSeconds(_timeBetweenWaves);
        }
    }

    private void StartNewWave()
    {
        _currentWave++;
        _isWaveActive = true;
        _enemiesRemaining = CalculateEnemyCount();

        Debug.Log($"Начало волны {_currentWave}. Врагов: {_enemiesRemaining}");
        StartCoroutine(SpawnEnemies(_enemiesRemaining));
    }

    private IEnumerator SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = FindValidSpawnPosition();
        if (spawnPos == Vector3.zero)
        {
            Debug.LogWarning("Не удалось найти валидную позицию для спавна врага!");
            return;
        }

        bool isRanged = Random.value > 0.7f;
        GameObject enemy = Instantiate(
            isRanged ? _enemySettings.rangedPrefab : _enemySettings.meleePrefab,
            spawnPos,
            Quaternion.identity
        );

        SetupEnemy(enemy, isRanged);
    }

    private void SetupEnemy(GameObject enemy, bool isRanged)
    {
        float health = _enemySettings.baseHealth * Mathf.Pow(_healthMultiplier, _currentWave - 1);
        float damage = _enemySettings.baseDamage * Mathf.Pow(_damageMultiplier, _currentWave - 1);
        float speed = _enemySettings.baseSpeed * Mathf.Pow(_speedMultiplier, _currentWave - 1);

        var healthComp = enemy.GetComponent<EnemyHealth>();
        if (healthComp != null)
        {
            healthComp.CurrentHealth = health;
            healthComp.OnDeath += () => {
                _enemiesRemaining--;
                Debug.Log($"Осталось врагов: {_enemiesRemaining}");
            };
        }

        if (isRanged)
        {
            var rangedAI = enemy.GetComponent<EnemyRangedAI>();
            if (rangedAI != null)
            {
                rangedAI.damage = damage;
            }
        }
        else
        {
            var meleeAI = enemy.GetComponent<EnemyMeleeAI>();
            if (meleeAI != null)
            {
                meleeAI.damage = damage;
                meleeAI.moveSpeed = speed;
            }
        }
    }

    private int CalculateEnemyCount()
    {
        return Mathf.FloorToInt(_baseEnemiesPerWave * Mathf.Pow(_enemyCountMultiplier, _currentWave - 1));
    }

    private Vector3 FindValidSpawnPosition()
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPos = _player.position + new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));

            float distance = Vector3.Distance(randomPos, _player.position);
            if (distance > 15f && distance < 25f)
            {
                // Проверка на препятствия
                if (!Physics.CheckSphere(randomPos, 1f, _obstacleLayers))
                {
                    // Проверка, что точка находится на нужном слое
                    RaycastHit hit;
                    if (Physics.Raycast(randomPos + Vector3.up * 10f, Vector3.down, out hit, 20f, _spawnSurfaceLayer))
                    {
                        // Дополнительная проверка, что под точкой нет препятствий
                        if (!Physics.Raycast(hit.point + Vector3.up * 0.5f, Vector3.down, 1f, _obstacleLayers))
                        {
                            return hit.point;
                        }
                    }
                }
            }
        }
        Debug.LogWarning("Не удалось найти валидную позицию после 30 попыток");
        return Vector3.zero;
    }
}