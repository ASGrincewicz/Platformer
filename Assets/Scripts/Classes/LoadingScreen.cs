using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Veganimus.Platformer
{
    public class LoadingScreen: MonoBehaviour
    {
        [SerializeField] private Image _progessBar;
        [SerializeField] private SceneLoader _loader;
        [SerializeField] private TMP_Text _loadingProgress;
        public byte sceneIndex;

        private void Start() => StartCoroutine(LoadLevelASync());


        private IEnumerator LoadLevelASync()
        {
            sceneIndex = _loader.sceneIndex;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

            while (asyncOperation.isDone == false)
            {
                _progessBar.fillAmount = asyncOperation.progress;
                _loadingProgress.text = $"Loading: {asyncOperation.progress * 100}%";
                yield return new WaitForEndOfFrame();
            }
        }
    }
}