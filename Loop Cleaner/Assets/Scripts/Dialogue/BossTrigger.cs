using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Collider))]
public class TriggerActivation : MonoBehaviour
{
    [Header("OST Settings")]
    [SerializeField] private AudioSource _ostAudioSource;
    [SerializeField] private bool _playOST = true;

    [Header("Door Settings")]
    [SerializeField] private GameObject _doorObject;
    [SerializeField] private bool _activateDoor = true;

    [Header("Prefabs to Destroy")]
    [SerializeField] private GameObject _prefabToDestroy1;
    [SerializeField] private GameObject _prefabToDestroy2;

    [Header("Trigger Settings")]
    [SerializeField] private bool _oneTimeUse = true;
    private bool _alreadyTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_oneTimeUse && _alreadyTriggered) return;
        if (!other.CompareTag("Player")) return;

        _alreadyTriggered = true;

        // Управление OST
        if (_ostAudioSource != null)
        {
            if (_playOST) _ostAudioSource.Play();
            else _ostAudioSource.Stop();
        }

        // Управление дверью
        if (_doorObject != null)
        {
            _doorObject.SetActive(_activateDoor);
        }

        // Уничтожение префабов
        DestroyPrefabInstances();
    }

    private void DestroyPrefabInstances()
    {
        if (_prefabToDestroy1 != null) DestroyAllInstances(_prefabToDestroy1);
        if (_prefabToDestroy2 != null) DestroyAllInstances(_prefabToDestroy2);
    }

    private void DestroyAllInstances(GameObject prefab)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
#if UNITY_EDITOR
            if (PrefabUtility.GetCorrespondingObjectFromSource(obj) == prefab)
#else
            if (obj.name.StartsWith(prefab.name + "(Clone)"))
#endif
            {
                Destroy(obj);
            }
        }
    }

    private void OnValidate()
    {
        // Настройка триггера
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }
}