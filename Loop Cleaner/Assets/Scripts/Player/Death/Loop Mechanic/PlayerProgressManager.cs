using System;
using UnityEngine;

[Serializable]
public class PlayerProgress
{
    public int Level;
    public float Damage;
    public float Speed;
    // �������� ������ ��������� ��������
}

public class ProgressManager : MonoBehaviour
{
    public static PlayerProgress CurrentProgress { get; private set; }

    private void Awake()
    {
        // ��������� �������� (����� ����� ���� �������� �� �����)
        CurrentProgress = new PlayerProgress();
    }
}