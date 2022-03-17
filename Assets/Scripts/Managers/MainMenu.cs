using UnityEngine;
namespace Veganimus.Platformer
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private ExitButton exitButton;
        [SerializeField] private SceneLoader _sceneLoader;

        public void LoadScene(int sceneIndex) => _sceneLoader.LoadGame(sceneIndex);
    }
}