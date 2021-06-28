using UnityEditor;
using UnityEngine;
namespace Veganimus.Platformer
{
    [CreateAssetMenu(menuName ="Exit Button")]
    public class ExitButton : ScriptableObject
    {
        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
        }
    }
}