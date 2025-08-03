using UnityEngine;

public class BossDialogueTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform bossCameraPoint;
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private PlayerMovementController playerController;
    [SerializeField] private BossController bossController;

    private bool _triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered || !other.CompareTag("Player")) return;

        _triggered = true;
        StartDialogue();
    }

    private void StartDialogue()
    {
        playerController.SetControlEnabled(false);

        CameraController.Instance.SwitchToDialogueCamera(bossCameraPoint);

        dialogueUI.SetActive(true);
        dialogueSystem.StartDialogue(EndDialogue);
    }

    private void EndDialogue()
    {
        CameraController.Instance.SwitchToPlayerCamera();

        dialogueUI.SetActive(false);

        playerController.SetControlEnabled(true);

        bossController.ActivateBoss();
    }
}