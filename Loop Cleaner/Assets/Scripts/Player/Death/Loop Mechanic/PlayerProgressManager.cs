using System;
using UnityEngine;

[Serializable]
public class PlayerProgress
{
    public int Level;
    public float Damage;
    public float Speed;
    // Добавьте другие параметры прокачки
}

public class ProgressManager : MonoBehaviour
{
    public static PlayerProgress CurrentProgress { get; private set; }

    private void Awake()
    {
        // Загружаем прогресс (здесь может быть загрузка из файла)
        CurrentProgress = new PlayerProgress();
    }
}