using UnityEngine;

public class BulletPhysics : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private GameObject _hitEffect;

    private void OnCollisionEnter(Collision collision)
    {
        var health = collision.collider.GetComponent<EnemyHealth>();
        health?.TakeDamage(_damage);

        if (_hitEffect != null)
        {
            Instantiate(_hitEffect, collision.contacts[0].point,
                       Quaternion.LookRotation(collision.contacts[0].normal));
        }

        Destroy(gameObject);
    }
}