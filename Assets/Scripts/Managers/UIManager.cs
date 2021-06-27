// Aaron Grincewicz ASGrincewicz@icloud.com 6/11/2021
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

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
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _missilesText;
        [SerializeField] private TMP_Text _collectibleText;
        [SerializeField] private List<Image> _livesImages = new List<Image>();
        [SerializeField] private List<Image> _bombImages = new List<Image>();
        private int _currentHealth;
        private int _maxHealth;
        private int _currentLives = 3;
        private int _maxLives;
        private int _missileCount;
        private int _maxMissileCount;
        private int _currentBombs;
        private int _maxBombs;
        private int _collectiblesCollected;
        private void Awake()
        {
            _instance = this;
        }
        private void Start()
        {
            LivesUpdate(_currentLives);
        }
        public void HealthTextUpdate(int amount)
        {
            _currentHealth = amount;
            _healthText.text = $"Health: {_currentHealth}";
        }
        public void LivesUpdate(int amount)
        {
            _currentLives = amount;
            ClearAmount(_livesImages);
            ShowAmount(0,_currentLives);
        }
       
        public void MissilesTextUpdate(int amount)
        {
            _missileCount = amount;
            _missilesText.text = $"Missiles: {_missileCount}";
        }
        public void BombUpdate(int amount)
        {
            _currentBombs = amount;
            ClearAmount(_bombImages);
            ShowAmount(1, amount);
        }
        public void CollectibleTextUpdate(int amount)
        {
            _collectiblesCollected += amount;
            _collectibleText.text = $"{_collectiblesCollected}";
        }
        private void ClearAmount(List<Image> toClear)
        {
            foreach (var image in toClear)
            {
                image.canvasRenderer.SetAlpha(0);
            }
        }
        private void ShowAmount(int toShow, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                switch(toShow)
                {
                    case 0://Lives
                        _livesImages[i].canvasRenderer.SetAlpha(1);
                        break;
                    case 1://Bombs
                        _bombImages[i].canvasRenderer.SetAlpha(1);
                        break;
                }
            }
        }
    }
}
