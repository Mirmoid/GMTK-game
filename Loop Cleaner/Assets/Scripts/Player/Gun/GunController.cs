using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Shoot Settings")]
    [SerializeField] private float _fireRate = 0.2f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _range = 1000f;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private GameObject _firePoint;

    [Header("Effects")]
    [SerializeField] private AudioSource _shotSound;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private TrailRenderer _bulletTrailPrefab;

    [Header("Recoil")]
    [SerializeField] private float _recoilForce = 1f;
    [SerializeField] private float _recoilRecoverySpeed = 5f;

    private float _nextFireTime;
    private Vector3 _currentRecoil;
    [SerializeField] private Camera _playerCamera;

    private void Awake()
    {
        _playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + _fireRate;
        }
    }

    private void Shoot()
    {
        if (_shotSound != null) _shotSound.Play();

        Debug.Log("Shoot");

        Vector3 shootDirection = _playerCamera.transform.forward;
        shootDirection += _playerCamera.transform.TransformDirection(_currentRecoil) * 0.01f;

        RaycastHit hit;
        if (Physics.Raycast(_playerCamera.transform.position, shootDirection, out hit, float.MaxValue))
        {
            TrailRenderer trailInstance = Instantiate(_bulletTrailPrefab, _firePoint.transform.position, Quaternion.identity);

            ProcessHit(hit);

            StartCoroutine(SpwanTrail(trailInstance, hit));
        }
    }

    private void ProcessHit(RaycastHit hit)
    {
        var health = hit.collider.GetComponent<EnemyHealth>();
        health?.TakeDamage(_damage);

        if (_hitEffect != null)
        {
            Instantiate(_hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    private IEnumerator SpwanTrail(TrailRenderer trailRenderer, RaycastHit Hit)
    {

        float time = 0f;
        Vector3 startPos = trailRenderer.transform.position;

        while (time < 1)
        {
            trailRenderer.transform.position = Vector3.Lerp(startPos, Hit.point, time);
            time += Time.deltaTime / trailRenderer.time;

            yield return null;
        }

        trailRenderer.transform.position = Hit.point;

        if (_hitEffect != null)
        {
            Instantiate(_hitEffect, Hit.point, Quaternion.LookRotation(Hit.normal));
        }

        Destroy(trailRenderer.gameObject, trailRenderer.time);
    }
}