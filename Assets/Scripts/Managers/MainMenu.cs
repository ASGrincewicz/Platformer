using UnityEngine;
namespace Veganimus.Platformer
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private ExitButton exitButton;

        public void LoadScene(int sceneIndex)
        {
            _sceneLoader.LoadGame(sceneIndex);
        }
    }
}