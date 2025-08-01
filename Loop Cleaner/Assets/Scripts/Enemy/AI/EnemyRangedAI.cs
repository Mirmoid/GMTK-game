using UnityEngine;

public class EnemyRangedAI : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    public float damage;
    public float preferredDistance = 10f;
    public float attackCooldown = 2f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    private Transform _player;
    private float _lastAttackTime;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (_player == null) return;

        Vector3 direction = _player.position - transform.position;
        direction.y = 0;
        float distance = direction.magnitude;

        transform.rotation = Quaternion.LookRotation(direction);

        if (distance > preferredDistance * 1.1f)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else if (distance < preferredDistance * 0.9f)
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (Time.time > _lastAttackTime + attackCooldown)
        {
            Shoot();
            _lastAttackTime = Time.time;
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<EnemyProjectile>().damage = damage;
    }
}