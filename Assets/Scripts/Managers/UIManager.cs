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
        [SerializeField] private GameObject _pauseMenu;
        private sbyte _currentHealth;
        private byte _maxHealth;
        private sbyte _currentLives;
        private byte _maxLives;
        private sbyte _missileCount;
        private byte _maxMissileCount;
        private sbyte _currentBombs;
        private byte _maxBombs;
        private byte _collectiblesCollected;

        private void Awake() => _instance = this;

        private void Start()
        {
            HealthTextUpdate(_currentHealth);
            LivesUpdate(_currentLives);
        }
        public void ActivatePauseMenu(bool isActive) => _pauseMenu.SetActive(isActive);

        public void HealthTextUpdate(sbyte amount)
        {
            _currentHealth = amount;
            _healthText.text = $"Health: {_currentHealth}";
        }
        public void LivesUpdate(sbyte amount)
        {
            _currentLives = (sbyte)(amount - 1);
            ClearAmount(_livesImages);
            ShowAmount((byte)0,_currentLives);
        }
       
        public void MissilesTextUpdate(sbyte amount)
        {
            _missileCount = amount;
            _missilesText.text = $"Missiles: {_missileCount}";
        }
        public void BombUpdate(sbyte amount)
        {
            _currentBombs = amount;
            ClearAmount(_bombImages);
            ShowAmount(1, amount);
        }
        public void CollectibleTextUpdate(byte amount)
        {
            _collectiblesCollected += amount;
            _collectibleText.text = $"{_collectiblesCollected}";
        }
        private void ClearAmount(List<Image> toClear)
        {
            foreach (Image image in toClear)
                image.canvasRenderer.SetAlpha(0);
            
        }
        private void ShowAmount(byte toShow, sbyte amount)
        {
            for (byte i = 0; i < amount; i++)
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
