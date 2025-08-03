using UnityEngine;

public class MenuCompositionRoot : MonoBehaviour
{
    [SerializeField] private MenuView _menuView;

    private IMenuController _menuController;

    private void Awake()
    {
        // ������� �����������
        ISceneLoader sceneLoader = new SceneLoader();
        IAudioManager audioManager = new AudioManager();

        // ������� ����������
        _menuController = new MenuController(_menuView, sceneLoader, audioManager);

        // ��������������
        _menuController.Initialize();
    }
}