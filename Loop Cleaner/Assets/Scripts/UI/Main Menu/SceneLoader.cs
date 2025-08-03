using UnityEngine.SceneManagement;

public class SceneLoader : ISceneLoader
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}