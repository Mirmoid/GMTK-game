using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Shoot settings")]
    [SerializeField] private float _fireRate = 0.2f;
    [SerializeField] private float _bulletSpeed = 50f;
    [SerializeField] private float _bulletLifetime = 2f;
    [SerializeField] private Transform _firePoint;

    [Header("Effects")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _bulletTrailPrefab;
    [SerializeField] private float _trailWidth = 0.1f;
    [SerializeField] private float _trailFadeSpeed = 2f;

    [Header("Other effects")]
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private AudioSource _shotSound;

    private float _nextFireTime;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + _fireRate;
        }
    }

    private void Shoot()
    {
        if (_muzzleFlash != null) _muzzleFlash.Play();
        if (_shotSound != null) _shotSound.Play();

        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.linearVelocity = _firePoint.forward * _bulletSpeed;

        GameObject trail = Instantiate(_bulletTrailPrefab, _firePoint.position, Quaternion.identity);
        BulletTrail trailController = trail.GetComponent<BulletTrail>();
        trailController.Initialize(bullet.transform, _bulletSpeed, _trailWidth, _trailFadeSpeed);

        Destroy(bullet, _bulletLifetime);
    }
}