using UnityEngine;

public class MenuCompositionRoot : MonoBehaviour
{
    [SerializeField] private MenuView _menuView;

    private IMenuController _menuController;

    private void Awake()
    {
        // Создаем зависимости
        ISceneLoader sceneLoader = new SceneLoader();
        IAudioManager audioManager = new AudioManager();

        // Создаем контроллер
        _menuController = new MenuController(_menuView, sceneLoader, audioManager);

        // Инициализируем
        _menuController.Initialize();
    }
}