using UnityEngine;

public class LevelResetManager : MonoBehaviour
{
    [SerializeField] private Transform[] _resettableObjects; // Объекты, которые нужно сбросить
    private Vector3[] _originalPositions;
    private Quaternion[] _originalRotations;

    private void Awake()
    {
        // Сохраняем начальные позиции всех объектов
        _originalPositions = new Vector3[_resettableObjects.Length];
        _originalRotations = new Quaternion[_resettableObjects.Length];

        for (int i = 0; i < _resettableObjects.Length; i++)
        {
            _originalPositions[i] = _resettableObjects[i].position;
            _originalRotations[i] = _resettableObjects[i].rotation;
        }
    }

    public void ResetLevel()
    {
        // Возвращаем объекты на начальные позиции
        for (int i = 0; i < _resettableObjects.Length; i++)
        {
            _resettableObjects[i].position = _originalPositions[i];
            _resettableObjects[i].rotation = _originalRotations[i];
        }
    }
}