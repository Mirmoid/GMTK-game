using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool InteractPressed => Input.GetKeyDown(KeyCode.E);
}