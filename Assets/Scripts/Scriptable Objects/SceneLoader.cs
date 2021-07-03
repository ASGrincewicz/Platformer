using UnityEngine;
using UnityEngine.SceneManagement;
namespace Veganimus.Platformer
{
    [CreateAssetMenu(menuName = "Scene Loader")]
    public class SceneLoader : ScriptableObject
    {
        public byte sceneIndex;

        public void LoadGame(byte index)
        {
            sceneIndex = index;
            var toUnload = SceneManager.GetActiveScene().buildIndex;
            var load = SceneManager.LoadSceneAsync(2);
            if (load.isDone)
            {
                SceneManager.UnloadSceneAsync(toUnload);
            }
            if (Time.timeScale < 1)
                Time.timeScale = 1;
        }
    }
}