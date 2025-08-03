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
        // ���������� ���� ��� ������ ��������� �������
        _hasHit = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���������� ��� ������������ � ����� �����������
        DestroyTrail();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������� ��� ����� � �������
        DestroyTrail();
    }

    private void Update()
    {
        // ����������, ���� ����� ���������� � �� ���� ������������
        if (!_hasHit && _trail.positionCount <= 0.05)
        {
            DestroyTrail();
        }
    }

    private void DestroyTrail()
    {
        if (_hasHit) return; // ��� ������������ �����������

        _hasHit = true;

        // ��������� ��������� ������ ����� ������������
        _trail.emitting = false;
        _trail.autodestruct = true;

        // ���������� � ��������� ���������, ����� ����� ����� ������������
        Destroy(gameObject, _trail.time);
    }
}