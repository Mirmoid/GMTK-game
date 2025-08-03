using UnityEngine;

public interface IActivationSystem
{
    int ActivatedCount { get; }
    int TotalToActivate { get; }
    void RegisterActivatable(GameObject activatable);
    void OnObjectActivated();
    void CheckFinalActivation();
}