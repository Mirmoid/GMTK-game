using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    [SerializeField] private float _interactionDistance = 3f;

    public float InteractionDistance => _interactionDistance;

    public abstract void Interact();
}