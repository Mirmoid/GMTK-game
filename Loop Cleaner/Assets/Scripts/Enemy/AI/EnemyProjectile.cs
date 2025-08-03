using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Settings")]
    public float damage = 15f;
    public float speed = 10f;
    public float arcHeight = 2f;
    public GameObject impactEffect;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _progress;

    public void Setup(Vector3 target)
    {
        _startPosition = transform.position;
        _targetPosition = target;
        _progress = 0f;

        _targetPosition.y += 0.5f;
    }

    private void Update()
    {
        _progress += Time.deltaTime * speed / Vector3.Distance(_startPosition, _targetPosition);

        if (_progress >= 1f)
        {
            Impact();
            return;
        }

        float parabola = 1.0f - 4.0f * (_progress - 0.5f) * (_progress - 0.5f);
        Vector3 nextPos = Vector3.Lerp(_startPosition, _targetPosition, _progress);
        nextPos.y += parabola * arcHeight;

        transform.position = nextPos;

        if (_progress < 0.95f)
        {
            Vector3 moveDirection = (nextPos - transform.position).normalized;
            if (moveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovementController player = other.GetComponent<PlayerMovementController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Impact();
        }
    }

    private void Impact()
    {
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}