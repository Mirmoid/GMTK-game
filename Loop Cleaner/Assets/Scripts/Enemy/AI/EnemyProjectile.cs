using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage;
    public float speed = 20f;
    public float lifetime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}