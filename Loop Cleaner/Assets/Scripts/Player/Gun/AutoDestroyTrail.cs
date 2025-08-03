using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class AutoDestroyTrail : MonoBehaviour
{
    private TrailRenderer _trail;
    private bool _hasHit = false;

    private void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        // —брасываем флаг при каждом включении объекта
        _hasHit = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ”ничтожаем при столкновении с любым коллайдером
        DestroyTrail();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ”ничтожаем при входе в триггер
        DestroyTrail();
    }

    private void Update()
    {
        // ”ничтожаем, если трейл завершилс€ и не было столкновени€
        if (!_hasHit && _trail.positionCount <= 0.05)
        {
            DestroyTrail();
        }
    }

    private void DestroyTrail()
    {
        if (_hasHit) return; // ”же обрабатываем уничтожение

        _hasHit = true;

        // ќтключаем рендеринг трейла перед уничтожением
        _trail.emitting = false;
        _trail.autodestruct = true;

        // ”ничтожаем с небольшой задержкой, чтобы трейл успел дорисоватьс€
        Destroy(gameObject, _trail.time);
    }
}