using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private EnemyType _enemySettings;
    [SerializeField] private Transform _player;
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private int _baseEnemiesPerWave = 5;

    [Header("Progress")]
    [SerializeField] private float _enemyCountMultiplier = 1.2f;
    [SerializeField] private float _healthMultiplier = 1.15f;
    [SerializeField] private float _damageMultiplier = 1.1f;
    [SerializeField] private float _speedMultiplier = 1.05f;

    private int _currentWave = 0;
    private int _enemiesRemaining;
    private bool _isWaveActive;

    private void Start()
    {
        StartCoroutine(WaveCycle());
    }

    private IEnumerator WaveCycle()
    {
        while (true)
        {
            yield return new WaitUntil(() => !_isWaveActive || _enemiesRemaining <= 0);

            if (_currentWave > 0)
            {
                Debug.Log($"Волна {_currentWave} завершена! Подготовка к следующей...");
                yield return new WaitForSeconds(_timeBetweenWaves);
            }

            StartNewWave();
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
        if (spawnPos == Vector3.zero) return;

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
            if (distance > 15f && distance < 25f && !Physics.CheckSphere(randomPos, 1f, LayerMask.GetMask("Obstacle")))
            {
                return randomPos;
            }
        }
        return Vector3.zero;
    }
}