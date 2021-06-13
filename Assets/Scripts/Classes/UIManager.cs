// Aaron Grincewicz ASGrincewicz@icloud.com 6/11/2021
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Veganimus.Platformer
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton
        public static UIManager Instance
        {
            get
            {
                return _instance;
            }
        }
        private static UIManager _instance;
        #endregion
        [SerializeField] private TMP_Text _collectibleText;
        private int _collectiblesCollected;
        private void Awake()
        {
            _instance = this;
        }
        public void UpdateCollectibleText(int amount)
        {
            _collectiblesCollected += amount;
            _collectibleText.text = $"{_collectiblesCollected}";
        }
    }
   
}
