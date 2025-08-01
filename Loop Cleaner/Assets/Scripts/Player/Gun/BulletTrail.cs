using UnityEngine;
using System.Collections;

public class BulletTrail : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Transform _bulletTransform;
    private float _bulletSpeed;
    private float _fadeSpeed;
    private Vector3 _lastBulletPosition;
    private float _trailLength;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.startWidth = 0f;
        _lineRenderer.endWidth = 0f;
    }

    public void Initialize(Transform bullet, float bulletSpeed, float width, float fadeSpeed)
    {
        _bulletTransform = bullet;
        _bulletSpeed = bulletSpeed;
        _fadeSpeed = fadeSpeed;
        _lineRenderer.startWidth = width;
        _lineRenderer.endWidth = width;
        _lastBulletPosition = _bulletTransform.position;
    }

    private void Update()
    {
        if (_bulletTransform != null)
        {
            _lineRenderer.SetPosition(0, _bulletTransform.position);
            _lineRenderer.SetPosition(1, _lastBulletPosition);

            _trailLength = Vector3.Distance(_bulletTransform.position, _lastBulletPosition);
            _lastBulletPosition = _bulletTransform.position;
        }
        else
        {
            FadeTrail();
        }
    }

    private void FadeTrail()
    {
        if (_trailLength > 0)
        {
            float fadeAmount = _bulletSpeed * _fadeSpeed * Time.deltaTime;
            _trailLength = Mathf.Max(0, _trailLength - fadeAmount);

            Vector3 endPosition = _lineRenderer.GetPosition(0) + (_lineRenderer.GetPosition(1) - _lineRenderer.GetPosition(0)).normalized * _trailLength;
            _lineRenderer.SetPosition(1, endPosition);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}